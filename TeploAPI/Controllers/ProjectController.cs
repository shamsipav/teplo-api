using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Migrations;
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
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceProject projectPeriodFurnaceData, int? inputDataId)
        {
            var basePeriodFurnaceData = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id == inputDataId);
            var basePeriodFurnaceDataClear = basePeriodFurnaceData;

            // TODO: Добавить try, catch, log, return Error
            // Из базы
            var cokeCoefficients = await _context.Сoefficients.FirstOrDefaultAsync(i => i.Id == 1);
            var furnanceCapacityCoefficients = await _context.Сoefficients.FirstOrDefaultAsync(i => i.Id == 2);

            Reference reference = new Reference { CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            // TODO: Возможно, стоит вынести инициализацию сервиса.
            CalculateService calculate = new CalculateService();

            var projectChangedInputData = new Furnace();
            
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

                basePeriodFurnaceData.BlastTemperature = projectChangedInputData.BlastTemperature;
                basePeriodFurnaceData.BlastHumidity = projectChangedInputData.BlastHumidity;
                basePeriodFurnaceData.OxygenContentInBlast = projectChangedInputData.OxygenContentInBlast;
                basePeriodFurnaceData.ColoshGasPressure = projectChangedInputData.ColoshGasPressure;
                basePeriodFurnaceData.NaturalGasConsumption = projectChangedInputData.NaturalGasConsumption;
                basePeriodFurnaceData.Chugun_SI = projectChangedInputData.Chugun_SI;
                basePeriodFurnaceData.Chugun_MN = projectChangedInputData.Chugun_MN;
                basePeriodFurnaceData.Chugun_P = projectChangedInputData.Chugun_P;
                basePeriodFurnaceData.Chugun_S = projectChangedInputData.Chugun_S;
                basePeriodFurnaceData.AshContentInCoke = projectChangedInputData.AshContentInCoke;
                basePeriodFurnaceData.SulfurContentInCoke = projectChangedInputData.SulfurContentInCoke;

                // Расчет теплового режима в базовом периоде и проектном периодах
                var baseResultData = new Result();
                var projectResultData = new Result();

                try
                {
                    baseResultData = calculate.СalculateThermalRegime(basePeriodFurnaceDataClear);
                    projectResultData = calculate.СalculateThermalRegime(basePeriodFurnaceData);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/project Ошибка выполнения расчета теплового режима в базовом периоде: {ex}");
                    return Problem($"Не удалось выполнить теплового режима в базовом периоде: {ex}");
                }

                var baseResult = new ResultViewModel { Input = basePeriodFurnaceDataClear, Result = baseResultData };
                var projectResult = new ResultViewModel { Input = basePeriodFurnaceData, Result = projectResultData };

                var result = new ProjectResultViewModel { BaseResult = baseResult, ProjectResult = projectResult };

                return Ok(result);
            }

            return NotFound("Не удалось найти информацию об варианте расчета");

            //ProjectCalculateModel projectResult = new ProjectCalculateModel(inputData, projectView.Project, reference);
        }
    }
}
