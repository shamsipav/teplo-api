using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private TeploDBContext _context;
        public ProjectController(TeploDBContext context)
        {
            _context = context;
        }

        // TODO: ПРОВЕРИТЬ РАБОТЕТ ЛИ КОРРЕКТНО ПРОЕКТНЫЙ РЕЖИМ
        // TODO: Refactoring.
        // TODO: Стоит поменять inputDataId на basePeriodFurnaceData?
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceProject projectPeriodFurnaceData, int? inputDataId)
        {
            var basePeriodFurnaceData = new Furnace();
            var basePeriodFurnaceDataClear = new Furnace();

            try
            {
                // TODO: Бессмысленное повторное обращение?
                basePeriodFurnaceData = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(d => d.Id == inputDataId);
                basePeriodFurnaceDataClear = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(d => d.Id == inputDataId);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/project Ошибка получения набора исходных данных в базовом периоде: {ex}");
                return Problem($"Не удалось получить набор исходных данных для базового периода: {ex}");
            }

            // TODO: Добавить try, catch, log, return Error
            // Из базы
            var cokeCoefficients = new Сoefficients();
            var furnanceCapacityCoefficients = new Сoefficients();

            try
            {
                cokeCoefficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 1);
                furnanceCapacityCoefficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 2);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/project Ошибка получения коэффициентов для справочника: {ex}");
                return Problem($"Не удалось получить коэффициенты для справочника: {ex}");
            }

            Reference reference = new Reference { CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            CalculateService calculate = new CalculateService();

            var projectChangedInputData = new ProjectDataViewModel();
            
            if (basePeriodFurnaceData!= null && basePeriodFurnaceDataClear != null)
            {
                try
                {
                    projectChangedInputData = calculate.CalculateProjectThermalRegime(basePeriodFurnaceData, projectPeriodFurnaceData, reference);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/project Ошибка выполнения корректировки исходных данных в проектном периоде: {ex}");
                    return Problem($"Не удалось выполнить корректировку исходных данных в проектном периоде: {ex}");
                }

                basePeriodFurnaceData.BlastTemperature = projectChangedInputData.ProjectInputData.BlastTemperature;
                basePeriodFurnaceData.BlastHumidity = projectChangedInputData.ProjectInputData.BlastHumidity;
                basePeriodFurnaceData.OxygenContentInBlast = projectChangedInputData.ProjectInputData.OxygenContentInBlast;
                basePeriodFurnaceData.ColoshGasPressure = projectChangedInputData.ProjectInputData.ColoshGasPressure;
                basePeriodFurnaceData.NaturalGasConsumption = projectChangedInputData.ProjectInputData.NaturalGasConsumption;
                basePeriodFurnaceData.Chugun_SI = projectChangedInputData.ProjectInputData.Chugun_SI;
                basePeriodFurnaceData.Chugun_MN = projectChangedInputData.ProjectInputData.Chugun_MN;
                basePeriodFurnaceData.Chugun_P = projectChangedInputData.ProjectInputData.Chugun_P;
                basePeriodFurnaceData.Chugun_S = projectChangedInputData.ProjectInputData.Chugun_S;
                basePeriodFurnaceData.AshContentInCoke = projectChangedInputData.ProjectInputData.AshContentInCoke;
                basePeriodFurnaceData.SulfurContentInCoke = projectChangedInputData.ProjectInputData.SulfurContentInCoke;

                basePeriodFurnaceData.SpecificConsumptionOfCoke = projectChangedInputData.ChangedInputData.SpecificConsumptionOfCoke;
                basePeriodFurnaceData.DailyСapacityOfFurnace = projectChangedInputData.ChangedInputData.DailyСapacityOfFurnace;

                // Расчет теплового режима в базовом и проектном периодах
                var baseResultData = new Result();
                var projectResultData = new Result();

                try
                {
                    baseResultData = calculate.СalculateThermalRegime(basePeriodFurnaceDataClear);
                    projectResultData = calculate.СalculateThermalRegime(basePeriodFurnaceData);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/project Ошибка выполнения расчета в проектном периоде: {ex}");
                    return Problem($"Не удалось выполнить расчет в базовом периоде: {ex}");
                }

                var baseResult = new ResultViewModel { Input = basePeriodFurnaceDataClear, Result = baseResultData };
                var projectResult = new ResultViewModel { Input = basePeriodFurnaceData, Result = projectResultData };

                var result = new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = projectResult };

                return Ok(result);
            }

            return NotFound("Не удалось найти информацию об варианте расчета");
        }
    }
}
