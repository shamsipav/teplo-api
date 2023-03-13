using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IValidator<Furnace> _validator;
        private TeploDBContext _context;
        public BaseController(TeploDBContext context, IValidator<Furnace> validator)
        {
            _context = context;
            _validator = validator;
        }

        // TODO: Добавить модель в параметры
        // TODO: Добавить try, catch, FluentValidation
        // TODO: Добавить названия для сохраненных вариантов исходных данных
        // TODO: Добавить класс Response для унификации результатов методов
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

            // Remove this code
            furnace = Furnace.GetDefaultData();

            if (save)
            {
                try
                {
                    _context.Furnaces.Add(furnace);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Ошибка сохранения варианта исходных данных: {ex}");
                }
            }
            // TODO: Возможно, стоит вынести инициализацию сервиса
            CalculateService calculate = new CalculateService();

            var calculateResult = calculate.СalculateThermalRegime(furnace);

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(result);
        }

        // TODO: try - catch
        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(int basePeriodId, int comparativePeriodId)
        {
            CalculateService calculate = new CalculateService();

            // Расчет теплового режима в базовом отчетном периоде
            var basePeriodFurnance = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(f => f.Id == basePeriodId);
            var calculateBaseResult = new Result();

            if (basePeriodFurnance != null)
                calculateBaseResult = calculate.СalculateThermalRegime(basePeriodFurnance);

            var baseResult = new ResultViewModel { Input = basePeriodFurnance, Result = calculateBaseResult };

            // Расчет теплового режима в сравнительном отчетном периоде
            var comparativePeriodFurnance = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(f => f.Id == comparativePeriodId);
            var calculateComparativeResult = new Result();

            if (comparativePeriodFurnance != null)
                calculateComparativeResult = calculate.СalculateThermalRegime(comparativePeriodFurnance);

            var comparativeResult = new ResultViewModel { Input = comparativePeriodFurnance, Result = calculateComparativeResult };

            // Объединение результатов расчетов
            var comparison = new ComparisonViewModel { BasePeriodResult = baseResult, СomparativePeriodResult = comparativeResult };

            return Ok(comparison);
        }
    }
}
