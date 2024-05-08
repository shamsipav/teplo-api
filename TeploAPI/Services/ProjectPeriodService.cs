using System.Security.Claims;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils.Extentions;
using TeploAPI.ViewModels;

namespace TeploAPI.Services;

public class ProjectPeriodService : IProjectPeriodService
{
    private readonly IReferenceCoefficientsService _referenceService;
    private readonly IFurnaceService _furnaceService;
    private readonly ICalculateService _calculateService;
    private readonly IRepository<FurnaceBaseParam> _furnaceWorkParamRepository;

    public ProjectPeriodService(IRepository<FurnaceBaseParam> furnaceWorkParamRepository,
        IReferenceCoefficientsService referenceCoefficientsService,
        ICalculateService calculateService,
        IFurnaceService furnaceService)
    {
        _furnaceWorkParamRepository = furnaceWorkParamRepository;
        _referenceService = referenceCoefficientsService;
        _calculateService = calculateService;
        _furnaceService = furnaceService;
    }

    public async Task<UnionResultViewModel> ProcessProjectPeriod(FurnaceProjectParam projectPeriodFurnaceData, Guid inputDataId)
    {
        FurnaceBaseParam basePeriodParam = await _furnaceWorkParamRepository.GetSingleAsync(p => p.Id == inputDataId, false);

        if (basePeriodParam == null)
            throw new BusinessLogicException($"В базе данных нет сохраненных вариантов исходных данных с идентификатором {inputDataId}");

        FurnaceBaseParam basePeriodParamClear = await _furnaceWorkParamRepository.GetSingleAsync(p => p.Id == inputDataId, false);

        Reference reference = await _referenceService.GetCoefficientsReference();

        if (reference == null)
            throw new BusinessLogicException("Не удалось получить корректировочные коэффициенты");

        ValidateProjectPeriodData(projectPeriodFurnaceData, basePeriodParam);

        ProjectDataViewModel projectChangedInputData = _calculateService.CalculateProjectThermalRegime(basePeriodParam, projectPeriodFurnaceData, reference);

        basePeriodParam.BlastTemperature = projectChangedInputData.ProjectInputData.BlastTemperature;
        basePeriodParam.BlastHumidity = projectChangedInputData.ProjectInputData.BlastHumidity;
        basePeriodParam.OxygenContentInBlast = projectChangedInputData.ProjectInputData.OxygenContentInBlast;
        basePeriodParam.ColoshGasPressure = projectChangedInputData.ProjectInputData.ColoshGasPressure;
        basePeriodParam.NaturalGasConsumption = projectChangedInputData.ProjectInputData.NaturalGasConsumption;
        basePeriodParam.Chugun_SI = projectChangedInputData.ProjectInputData.Chugun_SI;
        basePeriodParam.Chugun_MN = projectChangedInputData.ProjectInputData.Chugun_MN;
        basePeriodParam.Chugun_P = projectChangedInputData.ProjectInputData.Chugun_P;
        basePeriodParam.Chugun_S = projectChangedInputData.ProjectInputData.Chugun_S;
        basePeriodParam.AshContentInCoke = projectChangedInputData.ProjectInputData.AshContentInCoke;
        basePeriodParam.SulfurContentInCoke = projectChangedInputData.ProjectInputData.SulfurContentInCoke;

        basePeriodParam.SpecificConsumptionOfCoke = projectChangedInputData.ChangedInputData.SpecificConsumptionOfCoke;
        basePeriodParam.DailyСapacityOfFurnace = projectChangedInputData.ChangedInputData.DailyСapacityOfFurnace;

        await UpdateInputDataByFurnace(basePeriodParamClear);
        Result baseResultData = _calculateService.СalculateThermalRegime(basePeriodParamClear);

        await UpdateInputDataByFurnace(basePeriodParam);
        Result projectResultData = _calculateService.СalculateThermalRegime(basePeriodParam);

        var baseResult = new ResultViewModel { Input = basePeriodParamClear, Result = baseResultData };
        var projectResult = new ResultViewModel { Input = basePeriodParam, Result = projectResultData };

        return new UnionResultViewModel { BaseResult = baseResult, ComparativeResult = projectResult };
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
    }

    // TODO: Реализовать через FluentValidation
    private void ValidateProjectPeriodData(FurnaceProjectParam projectPeriodData, FurnaceBaseParam basePeriodParam)
    {
        if (projectPeriodData.AshContentInCoke < 11 || projectPeriodData.AshContentInCoke > 12)
            throw new BadRequestException("Содержание золы в коксе должно быть в пределах от 11 до 12%");

        if (basePeriodParam.BlastHumidity != projectPeriodData.BlastHumidity)
            if (basePeriodParam.BlastTemperature != projectPeriodData.BlastTemperature)
                throw new BadRequestException("Изменение влажности дутья (г/м3) должно происходить без изменения температуры дутья (ºC)");
    }
}