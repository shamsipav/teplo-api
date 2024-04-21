﻿using Serilog;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Services;

public class BasePeriodService : IBasePeriodService
{
    private readonly IFurnaceWorkParamsService _furnaceWorkParamsService;
    private readonly ICalculateService _calculateService;
    private readonly IFurnaceService _furnaceService;

    public BasePeriodService(IFurnaceWorkParamsService furnaceWorkParamsService, ICalculateService calculateService,
        IFurnaceService furnaceService)
    {
        _furnaceWorkParamsService = furnaceWorkParamsService;
        _calculateService = calculateService;
        _furnaceService = furnaceService;
    }

    public async Task<ResultViewModel> ProcessBasePeriod(FurnaceBaseParam furnaceBase, bool saveData)
    {
        // Обновление существующего варианта исходных данных
        // Кейс отрабатывает при услови, когда передается вариант исходных данных, уже сохраненный до этого в БД,
        // Но с флагом save == true
        if (saveData && furnaceBase.SaveDate != DateTime.MinValue && furnaceBase.Day == DateTime.MinValue)
        {
            furnaceBase = await _furnaceWorkParamsService.UpdateAsync(furnaceBase);
        }

        // Сохранение нового варианта исходных данных
        if (saveData && furnaceBase.SaveDate == DateTime.MinValue && furnaceBase.Day == DateTime.MinValue)
        {
            // TODO: Перепроверить работоспособность
            furnaceBase = await _furnaceWorkParamsService.CreateOrUpdateAsync(furnaceBase);
        }

        // TODO: Проверить, авторизован ли пользователь перед этим действием
        await UpdateInputDataByFurnace(furnaceBase);

        Result calculateResult = _calculateService.СalculateThermalRegime(furnaceBase);

        return new ResultViewModel { Input = furnaceBase, Result = calculateResult };
    }

    public async Task<UnionResultViewModel> ProcessComparativePeriod(Guid basePeriodId, Guid comparativePeriodId)
    {
        // Получение наборов исходных данных для двух периодов.
        FurnaceBaseParam basePeriodFurnace = await _furnaceWorkParamsService.GetSingleAsync(basePeriodId);
        FurnaceBaseParam comparativePeriodFurnance = await _furnaceWorkParamsService.GetSingleAsync(comparativePeriodId);

        // Расчет теплового режима в базовом отчетном периоде.
        Result calculateBaseResult = new Result();

        if (basePeriodFurnace != null)
        {
            await UpdateInputDataByFurnace(basePeriodFurnace);
            calculateBaseResult = _calculateService.СalculateThermalRegime(basePeriodFurnace);
        }
        else
            throw new NotFoundException("Вариант исходных данных для базового периода не был найден");

        ResultViewModel baseResult = new ResultViewModel { Input = basePeriodFurnace, Result = calculateBaseResult };

        // Расчет теплового режима в сравнительном отчетном периоде.
        Result calculateComparativeResult = new Result();

        if (comparativePeriodFurnance != null)
        {
            await UpdateInputDataByFurnace(comparativePeriodFurnance);
            calculateComparativeResult = _calculateService.СalculateThermalRegime(comparativePeriodFurnance);
        }
        else
            throw new NotFoundException("Вариант исходных данных для сравнительного периода не был найден");

        ResultViewModel comparativeResult = new ResultViewModel { Input = comparativePeriodFurnance, Result = calculateComparativeResult };

        // Объединение и возвращение результатов расчетов.
        return new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = comparativeResult };
    }

    private async Task UpdateInputDataByFurnace(FurnaceBaseParam furnaceBase)
    {
        Furnace currentFurnace = await _furnaceService.GetSingleFurnaceAsync(furnaceBase.FurnaceId);

        if (currentFurnace != null)
        {
            furnaceBase.NumberOfFurnace = currentFurnace.NumberOfFurnace;
            furnaceBase.UsefulVolumeOfFurnace = currentFurnace.UsefulVolumeOfFurnace;
            furnaceBase.UsefulHeightOfFurnace = currentFurnace.UsefulHeightOfFurnace;
            furnaceBase.DiameterOfColoshnik = currentFurnace.DiameterOfColoshnik;
            furnaceBase.DiameterOfRaspar = currentFurnace.DiameterOfRaspar;
            furnaceBase.DiameterOfHorn = currentFurnace.DiameterOfHorn;
            furnaceBase.HeightOfHorn = currentFurnace.HeightOfHorn;
            furnaceBase.HeightOfTuyeres = currentFurnace.HeightOfTuyeres;
            furnaceBase.HeightOfZaplechiks = currentFurnace.HeightOfZaplechiks;
            furnaceBase.HeightOfRaspar = currentFurnace.HeightOfRaspar;
            furnaceBase.HeightOfShaft = currentFurnace.HeightOfShaft;
            furnaceBase.HeightOfColoshnik = currentFurnace.HeightOfColoshnik;
        }
        else
        {
            Log.Error($"HTTP POST api/base PostAsync: Данные о печи №{furnaceBase.NumberOfFurnace}) не найдены");
        }
    }
}