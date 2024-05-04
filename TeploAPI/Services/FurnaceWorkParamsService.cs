using System.Security.Claims;
using TeploAPI.Interfaces;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services;

public class FurnaceWorkParamsService : IFurnaceWorkParamsService
{
    private readonly IRepository<FurnaceBaseParam> _furnaceWorkParamsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FurnaceWorkParamsService(IRepository<FurnaceBaseParam> furnaceWorkParamsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _furnaceWorkParamsRepository = furnaceWorkParamsRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

    public async Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam)
    {
        furnaceBaseParam.Id = Guid.NewGuid();
        furnaceBaseParam.UserId = _user.GetUserId();
        furnaceBaseParam.SaveDate = DateTime.Now; // TODO: ToUtcNow();

        await _furnaceWorkParamsRepository.AddAsync(furnaceBaseParam);
        await _furnaceWorkParamsRepository.SaveChangesAsync();

        return furnaceBaseParam;
    }

    public async Task<FurnaceBaseParam> CreateOrUpdateAsync(FurnaceBaseParam dailyInfo)
    {
        // Если пришла дата, которая уже была для конкретной печи - обновляем суточную информацию
        FurnaceBaseParam existDaily = GetSingle(dailyInfo.FurnaceId, dailyInfo.Day);

        if (existDaily != null)
        {
            dailyInfo.Id = existDaily.Id;
            await UpdateAsync(dailyInfo);
        }
        else
        {
            await AddAsync(dailyInfo);
        }

        return dailyInfo;
    }

    public List<FurnaceBaseParam> GetAll(bool isDaily = false)
    {
        Guid userId = _user.GetUserId();
        return _furnaceWorkParamsRepository.Get(p => (isDaily ? p.Day != DateTime.MinValue : p.Day == DateTime.MinValue) && p.UserId == userId).ToList();
    }

    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id)
    {
        return await _furnaceWorkParamsRepository.GetByIdAsync(id);
    }

    public FurnaceBaseParam GetSingle(Guid id, DateTime day)
    {
        return _furnaceWorkParamsRepository.GetSingle(p => p.Id == id && p.Day == day);
    }

    /// <summary>
    /// Обновление прежде сохраненного варианта исходных данных
    /// </summary>
    /// <param name="furnace"></param>
    /// <returns></returns>
    public async Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam furnace)
    {
        FurnaceBaseParam existBaseParam = await _furnaceWorkParamsRepository.GetByIdAsync(furnace.Id);

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
        FurnaceBaseParam deletedFurnaceParam = await _furnaceWorkParamsRepository.DeleteAsync(id);
        await _furnaceWorkParamsRepository.SaveChangesAsync();

        return deletedFurnaceParam;
    }
}