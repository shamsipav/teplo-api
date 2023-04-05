using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SweetAPI.Models;
using SweetAPI.Utils;
using TeploAPI.Data;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private FurnaceService _furnaceService;
        private IValidator<Furnace> _validator;
        private TeploDBContext _context;
        public BaseController(TeploDBContext context, IValidator<Furnace> validator, FurnaceService furnaceService)
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
                return BadRequest(validationResult.Errors[0].ErrorMessage);

            // TODO: Вынести логику сохранения вариантов в отдельный сервис.
            if (furnace.Id > 0)
            {
                var updatedFurnaceId = await _furnaceService.UpdateFurnaceAsync(furnace);
                if (updatedFurnaceId == 0)
                    return Problem($"Не удалось обновить сохраненный вариант исходных данных с идентификатором id = '{furnace.Id}'");
            }

            if (save && furnace.Id == 0)
            {
                // TODO: Вынести в отдельный сервис код ниже отсюда (а также отрефакторить весь код с проверками на null):
                // TODO: А ЕЩЕ ЛУЧШЕ БУДЕТ В ТОКЕН ВШИТЬ ИДЕНТИФИКАТОР ПОЛЬЗОВАТЕЛЯ (а может и нет).
                var headers = Request.Headers;
                headers.TryGetValue("Authorization", out var authHeader);
                string token = authHeader.ToString().Split(' ').Skip(1).FirstOrDefault();

                string email = Validate.ValidateToken(token);

                var existUser = new User();

                if (email != null)
                {
                    existUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
                    if (existUser is null)
                        return NotFound("Пользователь с таким Email не найден");
                }
                else
                    return BadRequest("Некорректный токен");
                // до сюда.

                var savedFurnaceId = await _furnaceService.SaveFurnaceAsync(furnace, existUser.Id);
                if (savedFurnaceId == 0)
                    return Problem($"Не удалось сохранить новый вариант исходных данных");
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
                return Problem($"Не удалось выполнить расчет в базовом периоде: {ex}");
            }

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(result);
        }

        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(int basePeriodId, int comparativePeriodId)
        {
            if (basePeriodId == 0)
                return BadRequest("Необходимо указать вариант исходных данных для базового периода");

            if (comparativePeriodId == 0)
                return BadRequest("Необходимо указать вариант исходных данных для сравнительного периода");

            if (basePeriodId == comparativePeriodId)
                return BadRequest("Необходимо указать разные варианты исходных данных");

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
                return Problem($"Не удалось получить наборы исходных данных для двух периодов: {ex}");
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
                    return Problem($"Не удалось выполнить расчет в базовом периоде: {ex}");
                }
            }
            else
            {
                return NotFound("Вариант исходных данных для базового периода не был найден");
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
                    return Problem($"Не удалось выполнить расчет в сравнительном периоде периоде: {ex}");
                }
            }
            else
            {
                return NotFound("Вариант исходных данных для сравнительного периода не был найден");
            }

            var comparativeResult = new ResultViewModel { Input = comparativePeriodFurnance, Result = calculateComparativeResult };

            // Объединение результатов расчетов.
            var comparison = new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = comparativeResult };

            return Ok(comparison);
        }
    }
}
