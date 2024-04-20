using Microsoft.EntityFrameworkCore;
using TeploAPI.Interfaces;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories.Interfaces;

namespace TeploAPI.Services;

public class FurnaceWorkParamsService : IFurnaceWorkParamsService
{
    private IFurnaceWorkParamsRepository _furnaceWorkParamsRepository;

    public FurnaceWorkParamsService(IFurnaceWorkParamsRepository furnaceWorkParamsRepository)
    {
        _furnaceWorkParamsRepository = furnaceWorkParamsRepository;
    }

    public async Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam, Guid userId)
    {
        furnaceBaseParam.Id = Guid.NewGuid();
        furnaceBaseParam.UserId = userId;

        await _furnaceWorkParamsRepository.AddAsync(furnaceBaseParam);
        await _furnaceWorkParamsRepository.SaveChangesAsync();

        return furnaceBaseParam;
    }

    public async Task<FurnaceBaseParam> CreateOrUpdateAsync(FurnaceBaseParam dailyInfo, Guid userId)
    {
        // Если пришла дата, которая уже была для конкретной печи - обновляем суточную информацию
        FurnaceBaseParam existDaily = await GetSingleAsync(dailyInfo.FurnaceId, dailyInfo.Day);

        if (existDaily != null)
        {
            dailyInfo.Id = existDaily.Id;
            existDaily = await UpdateAsync(dailyInfo);
        }
        else
        {
            await AddAsync(dailyInfo, userId);
        }

        return existDaily;
    }

    public async Task<List<FurnaceBaseParam>> GetAll(Guid userId, bool isDaily = false)
    {
        return await _furnaceWorkParamsRepository.GetAll(userId, isDaily).ToListAsync();
    }

    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id)
    {
        return await _furnaceWorkParamsRepository.GetSingleAsync(id);
    }

    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id, DateTime day)
    {
        return await _furnaceWorkParamsRepository.GetSingleAsync(id, day);
    }

    /// <summary>
    /// Обновление прежде сохраненного варианта исходных данных
    /// </summary>
    /// <param name="furnace"></param>
    /// <returns></returns>
    public async Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam furnace)
    {
        FurnaceBaseParam existBaseParam = await _furnaceWorkParamsRepository.GetSingleAsync(furnace.Id);
        
        existBaseParam.NumberOfFurnace = furnace.NumberOfFurnace;
        existBaseParam.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
        existBaseParam.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
        existBaseParam.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
        existBaseParam.DiameterOfRaspar = furnace.DiameterOfRaspar;
        existBaseParam.DiameterOfHorn = furnace.DiameterOfHorn;
        existBaseParam.HeightOfHorn = furnace.HeightOfHorn;
        existBaseParam.HeightOfTuyeres = furnace.HeightOfTuyeres;
        existBaseParam.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
        existBaseParam.HeightOfRaspar = furnace.HeightOfRaspar;
        existBaseParam.HeightOfShaft = furnace.HeightOfShaft;
        existBaseParam.HeightOfColoshnik = furnace.HeightOfColoshnik;
        existBaseParam.EstablishedLevelOfEmbankment = furnace.EstablishedLevelOfEmbankment;
        existBaseParam.NumberOfTuyeres = furnace.NumberOfTuyeres;
        existBaseParam.DailyСapacityOfFurnace = furnace.DailyСapacityOfFurnace;
        existBaseParam.SpecificConsumptionOfCoke = furnace.SpecificConsumptionOfCoke;
        existBaseParam.SpecificConsumptionOfZRM = furnace.SpecificConsumptionOfZRM;
        existBaseParam.ShareOfPelletsInCharge = furnace.ShareOfPelletsInCharge;
        existBaseParam.BlastConsumption = furnace.BlastConsumption;
        existBaseParam.BlastTemperature = furnace.BlastTemperature;
        existBaseParam.BlastPressure = furnace.BlastPressure;
        existBaseParam.BlastHumidity = furnace.BlastHumidity;
        existBaseParam.OxygenContentInBlast = furnace.OxygenContentInBlast;
        existBaseParam.NaturalGasConsumption = furnace.NaturalGasConsumption;
        existBaseParam.ColoshGasTemperature = furnace.ColoshGasTemperature;
        existBaseParam.ColoshGasPressure = furnace.ColoshGasPressure;
        existBaseParam.ColoshGas_CO = furnace.ColoshGas_CO;
        existBaseParam.ColoshGas_CO2 = furnace.ColoshGas_CO2;
        existBaseParam.ColoshGas_H2 = furnace.ColoshGas_H2;
        existBaseParam.Chugun_SI = furnace.Chugun_SI;
        existBaseParam.Chugun_MN = furnace.Chugun_MN;
        existBaseParam.Chugun_P = furnace.Chugun_P;
        existBaseParam.Chugun_S = furnace.Chugun_S;
        existBaseParam.Chugun_C = furnace.Chugun_C;
        existBaseParam.AshContentInCoke = furnace.AshContentInCoke;
        existBaseParam.VolatileContentInCoke = furnace.VolatileContentInCoke;
        existBaseParam.SulfurContentInCoke = furnace.SulfurContentInCoke;
        existBaseParam.SpecificSlagYield = furnace.SpecificSlagYield;
        existBaseParam.HeatCapacityOfAgglomerate = furnace.HeatCapacityOfAgglomerate;
        existBaseParam.HeatCapacityOfPellets = furnace.HeatCapacityOfPellets;
        existBaseParam.HeatCapacityOfCoke = furnace.HeatCapacityOfCoke;
        existBaseParam.AcceptedTemperatureOfBackupZone = furnace.AcceptedTemperatureOfBackupZone;
        existBaseParam.ProportionOfHeatLossesOfLowerPart = furnace.ProportionOfHeatLossesOfLowerPart;
        existBaseParam.AverageSizeOfPieceCharge = furnace.AverageSizeOfPieceCharge;

        existBaseParam.HeatOfBurningOfNaturalGasOnFarms = furnace.HeatOfBurningOfNaturalGasOnFarms;
        existBaseParam.HeatOfIncompleteBurningCarbonOfCoke = furnace.HeatOfIncompleteBurningCarbonOfCoke;
        existBaseParam.TemperatureOfCokeThatCameToTuyeres = furnace.TemperatureOfCokeThatCameToTuyeres;
        existBaseParam.Day = furnace.Day;
        existBaseParam.SaveDate = DateTime.Now;

        existBaseParam.MaterialsWorkParamsList.Clear();
        existBaseParam.MaterialsWorkParamsList = furnace.MaterialsWorkParamsList;

        await _furnaceWorkParamsRepository.SaveChangesAsync();

        return existBaseParam;
    }

    public async Task<FurnaceBaseParam> RemoveAsync(Guid id)
    {
        FurnaceBaseParam deletedFurnaceParam = await _furnaceWorkParamsRepository.Delete(id);
        await _furnaceWorkParamsRepository.SaveChangesAsync();

        return deletedFurnaceParam;
    }
}