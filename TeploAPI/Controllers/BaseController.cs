﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories;
using TeploAPI.Services;
using TeploAPI.Utils;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class BaseController : ControllerBase
    {
        private IFurnaceService _furnaceService;
        private IFurnaceWorkParamsService _furnaceWorkParamsService;
        private IValidator<FurnaceBaseParam> _validator;
        private TeploDBContext _context;
        public BaseController(TeploDBContext context, IValidator<FurnaceBaseParam> validator, IFurnaceService furnaceService, IFurnaceWorkParamsService furnaceWorkParamsService)
        {
            _context = context;
            _validator = validator;
            _furnaceWorkParamsService = furnaceWorkParamsService;
        }

        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceBaseParam furnaceBase, bool save)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnaceBase);

            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            // Обновление существующего варианта исходных данных
            // Кейс отрабатывает при услови, когда передается вариант исходных данных, уже сохраненный до этого в БД,
            // Но с флагом save == true
            if (save && furnaceBase.SaveDate != DateTime.MinValue && furnaceBase.Day == DateTime.MinValue)
            {
                furnaceBase = await _furnaceWorkParamsService.UpdateAsync(furnaceBase);
            }

            // Сохранение нового варианта исходных данных
            if (save && furnaceBase.SaveDate == DateTime.MinValue && furnaceBase.Day == DateTime.MinValue)
            {
                // TODO: Не факт, что будет работать
                furnaceBase = await _furnaceWorkParamsService.CreateOrUpdateAsync(furnaceBase, User.GetUserId());
            }

            if (User.Identity.IsAuthenticated)
                // TODO: Реализовать более корректную связь
                // С UI нам пришел только идентификатор доменной печи и данные для расчета (FurnaceBaseParam furnaceBase)
                // Необходимо к этим данным добавить характеристики доменной печи
                await UpdateInputDataByFurnace(furnaceBase);

            // TODO: Возможно, стоит вынести инициализацию сервиса.
            CalculateService calculate = new CalculateService();

            var calculateResult = new Result();

            try
            {
                calculateResult = calculate.СalculateThermalRegime(furnaceBase);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/base Ошибка выполнения расчета: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось выполнить расчет в базовом периоде: {ex}" });
            }

            var result = new ResultViewModel { Input = furnaceBase, Result = calculateResult };

            return Ok(new Response { IsSuccess = true, Result = result });
        }

        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(string basePeriodId, string comparativePeriodId)
        {
            if (string.IsNullOrEmpty(basePeriodId))
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных / посуточную информацию для базового периода" });

            if (string.IsNullOrEmpty(comparativePeriodId))
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных / посуточную информацию для сравнительного периода" });

            if (basePeriodId == comparativePeriodId)
                return BadRequest(new Response { ErrorMessage = "Необходимо указать разные варианты данных или посуточной информации" });

            CalculateService calculate = new CalculateService();

            // Получение наборов исходных данных для двух периодов.
            var basePeriodFurnace = new FurnaceBaseParam();
            var comparativePeriodFurnance = new FurnaceBaseParam();

            try
            {
                basePeriodFurnace = await _context.FurnacesWorkParams.AsNoTracking().FirstOrDefaultAsync(f => f.Id.Equals(Guid.Parse(basePeriodId)));
                comparativePeriodFurnance = await _context.FurnacesWorkParams.AsNoTracking().FirstOrDefaultAsync(f => f.Id.Equals(Guid.Parse(comparativePeriodId)));
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
                    await UpdateInputDataByFurnace(basePeriodFurnace);
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
                    await UpdateInputDataByFurnace(comparativePeriodFurnance);
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

        // TODO: Вынести в отдельный класс (дублирование кода в ProjectController)
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
