using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SweetAPI.Models;
using SweetAPI.Utils;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private IFurnaceService _furnaceService;
        private IValidator<Furnace> _validator;
        private TeploDBContext _context;
        public BaseController(TeploDBContext context, IValidator<Furnace> validator, IFurnaceService furnaceService)
        {
            _context = context;
            _validator = validator;
            _furnaceService = furnaceService;
        }

        // TODO: Добавить названия для сохраненных вариантов исходных данных.
        // TODO: Добавить класс Response для унификации результатов методов.
        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        /// <param name="save">Сохранение варианта исзодных данных</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Furnace furnace, bool save)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnace);

            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            // TODO: Вынести логику сохранения вариантов в отдельный сервис.
            if (save == false && furnace.Id > 0)
            {
                var updatedFurnaceId = await _furnaceService.UpdateFurnaceAsync(furnace);
                if (updatedFurnaceId == 0)
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось обновить сохраненный вариант исходных данных с идентификатором id = '{furnace.Id}'" });
            }

            if (save)
            {
                int uid = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "uid").Value);
                if (uid == 0)
                    return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

                var savedFurnaceId = await _furnaceService.SaveFurnaceAsync(furnace, uid);
                if (savedFurnaceId == 0)
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось сохранить новый вариант исходных данных" });
            }

            // TODO: Возможно, стоит вынести инициализацию сервиса.
            CalculateService calculate = new CalculateService();

            var calculateResult = new Result();

            try
            {
                calculateResult = calculate.СalculateThermalRegime(furnace);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/base Ошибка выполнения расчета: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить расчет в базовом периоде: {ex}" });
            }

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(new Response { IsSuccess = true, Result = result });
        }

        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(int basePeriodId, int comparativePeriodId)
        {
            if (basePeriodId == 0)
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных для базового периода" });

            if (comparativePeriodId == 0)
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных для сравнительного периода" });

            if (basePeriodId == comparativePeriodId)
                return BadRequest(new Response { ErrorMessage = "Необходимо указать разные варианты исходных данных" });

            CalculateService calculate = new CalculateService();

            // Получение наборов исходных данных для двух периодов.
            var basePeriodFurnace = new Furnace();
            var comparativePeriodFurnance = new Furnace();

            try
            {
                basePeriodFurnace = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(f => f.Id == basePeriodId);
                comparativePeriodFurnance = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(f => f.Id == comparativePeriodId);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/base ComparisonAsync: Ошибка получения наборов исходных данных для двух периодов: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить наборы исходных данных для двух периодов: {ex}" });
            }

            // Расчет теплового режима в базовом отчетном периоде.
            var calculateBaseResult = new Result();

            if (basePeriodFurnace != null)
            {
                try
                {
                    calculateBaseResult = calculate.СalculateThermalRegime(basePeriodFurnace);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/base ComparisonAsync: Ошибка выполнения расчета: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить расчет в базовом периоде: {ex}" });
                }
            }
            else
            {
                return NotFound(new Response { ErrorMessage = "Вариант исходных данных для базового периода не был найден" });
            }

            var baseResult = new ResultViewModel { Input = basePeriodFurnace, Result = calculateBaseResult };

            // Расчет теплового режима в сравнительном отчетном периоде.
            var calculateComparativeResult = new Result();

            if (comparativePeriodFurnance != null)
            {
                try
                {
                    calculateComparativeResult = calculate.СalculateThermalRegime(comparativePeriodFurnance);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/base ComparisonAsync: Ошибка выполнения расчета: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить расчет в сравнительном периоде периоде: {ex}" });
                }
            }
            else
            {
                return NotFound(new Response { ErrorMessage = "Вариант исходных данных для сравнительного периода не был найден" });
            }

            var comparativeResult = new ResultViewModel { Input = comparativePeriodFurnance, Result = calculateComparativeResult };

            // Объединение результатов расчетов.
            var comparison = new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = comparativeResult };

            return Ok(new Response { IsSuccess = true, Result = comparison });
        }
    }
}
