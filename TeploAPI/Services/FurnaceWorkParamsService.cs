using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services;

public class FurnaceWorkParamsService : IFurnaceWorkParamsService
{
    private readonly TeploDBContext _dbContext;
    private readonly IRepository<FurnaceBaseParam> _furnaceWorkParamsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FurnaceWorkParamsService(IRepository<FurnaceBaseParam> furnaceWorkParamsRepository, 
        IHttpContextAccessor httpContextAccessor, TeploDBContext dbContext)
    {
        _furnaceWorkParamsRepository = furnaceWorkParamsRepository;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
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
        //FurnaceBaseParam existDaily = GetSingle(dailyInfo.FurnaceId, dailyInfo.Day);
        FurnaceBaseParam existDaily = _furnaceWorkParamsRepository.GetSingle(x => x.Id == dailyInfo.Id);

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
        if (isDaily)
        {
            return _furnaceWorkParamsRepository.GetWithInclude(w => w.UserId == userId && w.Day != DateTime.MinValue, p => p.MaterialsWorkParamsList).ToList();
        }
        else
        {
            return _furnaceWorkParamsRepository.Get(p => p.Day == DateTime.MinValue && p.UserId == userId).ToList();
        }
    }

    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id)
    {
        return _furnaceWorkParamsRepository.GetWithInclude(x => x.Id == id, p => p.MaterialsWorkParamsList).FirstOrDefault();
    }

    public FurnaceBaseParam GetSingle(Guid id, DateTime day)
    {
        return _furnaceWorkParamsRepository.GetSingle(p => p.Id == id && p.Day == day);
    }

    /// <summary>
    /// Обновление прежде сохраненного варианта исходных данных
    /// </summary>
    /// <returns></returns>
    public async Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam baseParam)
    {
        FurnaceBaseParam existBaseParam = _furnaceWorkParamsRepository
                                          .GetWithInclude(x => x.Id == baseParam.Id, p => p.MaterialsWorkParamsList)
                                          .FirstOrDefault();

        existBaseParam.NumberOfFurnace = baseParam.NumberOfFurnace;
        existBaseParam.UsefulVolumeOfFurnace = baseParam.UsefulVolumeOfFurnace;
        existBaseParam.UsefulHeightOfFurnace = baseParam.UsefulHeightOfFurnace;
        existBaseParam.DiameterOfColoshnik = baseParam.DiameterOfColoshnik;
        existBaseParam.DiameterOfRaspar = baseParam.DiameterOfRaspar;
        existBaseParam.DiameterOfHorn = baseParam.DiameterOfHorn;
        existBaseParam.HeightOfHorn = baseParam.HeightOfHorn;
        existBaseParam.HeightOfTuyeres = baseParam.HeightOfTuyeres;
        existBaseParam.HeightOfZaplechiks = baseParam.HeightOfZaplechiks;
        existBaseParam.HeightOfRaspar = baseParam.HeightOfRaspar;
        existBaseParam.HeightOfShaft = baseParam.HeightOfShaft;
        existBaseParam.HeightOfColoshnik = baseParam.HeightOfColoshnik;
        existBaseParam.EstablishedLevelOfEmbankment = baseParam.EstablishedLevelOfEmbankment;
        existBaseParam.NumberOfTuyeres = baseParam.NumberOfTuyeres;
        existBaseParam.DailyСapacityOfFurnace = baseParam.DailyСapacityOfFurnace;
        existBaseParam.SpecificConsumptionOfCoke = baseParam.SpecificConsumptionOfCoke;
        existBaseParam.SpecificConsumptionOfZRM = baseParam.SpecificConsumptionOfZRM;
        existBaseParam.ShareOfPelletsInCharge = baseParam.ShareOfPelletsInCharge;
        existBaseParam.BlastConsumption = baseParam.BlastConsumption;
        existBaseParam.BlastTemperature = baseParam.BlastTemperature;
        existBaseParam.BlastPressure = baseParam.BlastPressure;
        existBaseParam.BlastHumidity = baseParam.BlastHumidity;
        existBaseParam.OxygenContentInBlast = baseParam.OxygenContentInBlast;
        existBaseParam.NaturalGasConsumption = baseParam.NaturalGasConsumption;
        existBaseParam.ColoshGasTemperature = baseParam.ColoshGasTemperature;
        existBaseParam.ColoshGasPressure = baseParam.ColoshGasPressure;
        existBaseParam.ColoshGas_CO = baseParam.ColoshGas_CO;
        existBaseParam.ColoshGas_CO2 = baseParam.ColoshGas_CO2;
        existBaseParam.ColoshGas_H2 = baseParam.ColoshGas_H2;
        existBaseParam.Chugun_SI = baseParam.Chugun_SI;
        existBaseParam.Chugun_MN = baseParam.Chugun_MN;
        existBaseParam.Chugun_P = baseParam.Chugun_P;
        existBaseParam.Chugun_S = baseParam.Chugun_S;
        existBaseParam.Chugun_C = baseParam.Chugun_C;
        existBaseParam.AshContentInCoke = baseParam.AshContentInCoke;
        existBaseParam.VolatileContentInCoke = baseParam.VolatileContentInCoke;
        existBaseParam.SulfurContentInCoke = baseParam.SulfurContentInCoke;
        existBaseParam.SpecificSlagYield = baseParam.SpecificSlagYield;
        existBaseParam.HeatCapacityOfAgglomerate = baseParam.HeatCapacityOfAgglomerate;
        existBaseParam.HeatCapacityOfPellets = baseParam.HeatCapacityOfPellets;
        existBaseParam.HeatCapacityOfCoke = baseParam.HeatCapacityOfCoke;
        existBaseParam.AcceptedTemperatureOfBackupZone = baseParam.AcceptedTemperatureOfBackupZone;
        existBaseParam.ProportionOfHeatLossesOfLowerPart = baseParam.ProportionOfHeatLossesOfLowerPart;
        existBaseParam.AverageSizeOfPieceCharge = baseParam.AverageSizeOfPieceCharge;

        existBaseParam.HeatOfBurningOfNaturalGasOnFarms = baseParam.HeatOfBurningOfNaturalGasOnFarms;
        existBaseParam.HeatOfIncompleteBurningCarbonOfCoke = baseParam.HeatOfIncompleteBurningCarbonOfCoke;
        existBaseParam.TemperatureOfCokeThatCameToTuyeres = baseParam.TemperatureOfCokeThatCameToTuyeres;
        existBaseParam.Day = baseParam.Day;
        existBaseParam.SaveDate = DateTime.Now;

        if (existBaseParam.MaterialsWorkParamsList != null && existBaseParam.MaterialsWorkParamsList.Any())
        {
            existBaseParam.MaterialsWorkParamsList.Clear();
            existBaseParam.MaterialsWorkParamsList = baseParam.MaterialsWorkParamsList;
        }

        // TODO: Костыль, нужно реализовать в будущем корректное обновление
        // связанных сущностей в Generic Repository
        if (existBaseParam.MaterialsWorkParamsList != null && existBaseParam.MaterialsWorkParamsList.Any())
        {
            foreach (MaterialsWorkParams materialWorkParam in existBaseParam.MaterialsWorkParamsList)
            {
                // Обновление существующих элементов
                if (materialWorkParam.Id != Guid.Empty)
                {
                    _dbContext.Entry(materialWorkParam).State = EntityState.Modified;
                }
            }
        }

        await _furnaceWorkParamsRepository.UpdateAsync(existBaseParam);

        return existBaseParam;
    }

    public async Task<FurnaceBaseParam> RemoveAsync(Guid id)
    {
        FurnaceBaseParam deletedFurnaceParam = await _furnaceWorkParamsRepository.DeleteAsync(id);

        return deletedFurnaceParam;
    }
}