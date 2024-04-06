using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : TeploController
    {
        private IReferenceCoefficientsService _referenceService;
        private TeploDBContext _context;
        public ProjectController(TeploDBContext context, IReferenceCoefficientsService referenceService)
        {
            _context = context;
            _referenceService = referenceService;
        }

        // TODO: ПРОВЕРИТЬ РАБОТЕТ ЛИ КОРРЕКТНО ПРОЕКТНЫЙ РЕЖИМ
        // TODO: Refactoring.
        // TODO: Стоит поменять inputDataId на basePeriodFurnaceData?
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceProjectParam projectPeriodFurnaceData, Guid inputDataId)
        {
            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var basePeriodFurnaceData = new FurnaceBaseParam();
            var basePeriodFurnaceDataClear = new FurnaceBaseParam();

            try
            {
                // TODO: Бессмысленное повторное обращение?
                basePeriodFurnaceData = await _context.InputVariants.AsNoTracking().FirstOrDefaultAsync(d => d.Id.Equals(inputDataId));
                basePeriodFurnaceDataClear = await _context.InputVariants.AsNoTracking().FirstOrDefaultAsync(d => d.Id.Equals(inputDataId));
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/project Ошибка получения набора исходных данных в базовом периоде: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить набор исходных данных для базового периода" });
            }

            var reference = await _referenceService.GetCoefficientsReferenceByUserIdAsync(uid);

            if (reference == null)
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить коэффициенты для справочника" });

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
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить корректировку исходных данных в проектном периоде" });
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
                    await UpdateInputDataByFurnace(basePeriodFurnaceDataClear);
                    baseResultData = calculate.СalculateThermalRegime(basePeriodFurnaceDataClear);

                    await UpdateInputDataByFurnace(basePeriodFurnaceData);
                    projectResultData = calculate.СalculateThermalRegime(basePeriodFurnaceData);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/project Ошибка выполнения расчета в проектном периоде: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить расчет в базовом периоде" });
                }

                var baseResult = new ResultViewModel { Input = basePeriodFurnaceDataClear, Result = baseResultData };
                var projectResult = new ResultViewModel { Input = basePeriodFurnaceData, Result = projectResultData };

                var result = new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = projectResult };

                return Ok(new Response { IsSuccess = true, Result = result });
            }

            return NotFound(new Response { ErrorMessage = "Не удалось найти информацию об варианте расчета" });
        }

        // TODO: Вынести в отдельный класс (дублирование кода в BaseController)
        /// <summary>
        /// Добавление к классу с данными для расчета характеристик доменной печи из справочника доменных печей
        /// </summary>
        private async Task UpdateInputDataByFurnace(FurnaceBaseParam furnaceBase)
        {
            var furnace = new Furnace();

            try
            {
                furnace = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(f => f.Id == furnaceBase.FurnaceId);
                Console.WriteLine($"Получена печь {furnace?.NumberOfFurnace}");
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/base PostAsync: Не удалось получить данные о печи №{furnaceBase.NumberOfFurnace}: {ex}");
            }

            if (furnace != null)
            {
                furnaceBase.NumberOfFurnace = furnace.NumberOfFurnace;
                furnaceBase.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
                furnaceBase.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
                furnaceBase.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
                furnaceBase.DiameterOfRaspar = furnace.DiameterOfRaspar;
                furnaceBase.DiameterOfHorn = furnace.DiameterOfHorn;
                furnaceBase.HeightOfHorn = furnace.HeightOfHorn;
                furnaceBase.HeightOfTuyeres = furnace.HeightOfTuyeres;
                furnaceBase.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
                furnaceBase.HeightOfRaspar = furnace.HeightOfRaspar;
                furnaceBase.HeightOfShaft = furnace.HeightOfShaft;
                furnaceBase.HeightOfColoshnik = furnace.HeightOfColoshnik;
            }
            else
            {
                Log.Error($"HTTP POST api/base PostAsync: Данные о печи №{furnaceBase.NumberOfFurnace}) не найдены");
            }
        }
    }
}
