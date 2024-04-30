using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TeploAPI.Interfaces;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories.Interfaces;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services
{
    public class FurnaceService : IFurnaceService
    {
        private readonly IFurnaceRepository _furnaceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FurnaceService(IFurnaceRepository furnaceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _furnaceRepository = furnaceRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        
        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        public async Task<List<Furnace>> GetAll()
        {
            Guid userId = _user.GetUserId();
            return await _furnaceRepository.GetAll(userId).ToListAsync();
        }

        public async Task<Furnace> CreateFurnaceAsync(Furnace furnace)
        {
            furnace.UserId = _user.GetUserId();

            await _furnaceRepository.AddAsync(furnace);
            await _furnaceRepository.SaveChangesAsync();

            return furnace;
        }

        public async Task<Furnace> UpdateFurnaceAsync(Furnace furnace)
        {
            Furnace existFurnace = await _furnaceRepository.GetSingleAsync(furnace.Id);

            if (existFurnace == null)
            {
                return null;
            }

            existFurnace.NumberOfFurnace = furnace.NumberOfFurnace;
            existFurnace.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
            existFurnace.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
            existFurnace.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
            existFurnace.DiameterOfRaspar = furnace.DiameterOfRaspar;
            existFurnace.DiameterOfHorn = furnace.DiameterOfHorn;
            existFurnace.HeightOfHorn = furnace.HeightOfHorn;
            existFurnace.HeightOfTuyeres = furnace.HeightOfTuyeres;
            existFurnace.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
            existFurnace.HeightOfRaspar = furnace.HeightOfRaspar;
            existFurnace.HeightOfShaft = furnace.HeightOfShaft;
            existFurnace.HeightOfColoshnik = furnace.HeightOfColoshnik;

            _furnaceRepository.Update(furnace);
            await _furnaceRepository.SaveChangesAsync();

            return furnace;
        }

        public async Task<Furnace> GetSingleFurnaceAsync(Guid id)
        {
            return await _furnaceRepository.GetSingleAsync(id);
        }
        
        public async Task<Furnace> RemoveFurnaceAsync(Guid id)
        {
            Furnace deletedFurnace = await _furnaceRepository.Delete(id);
            await _furnaceRepository.SaveChangesAsync();

            return deletedFurnace;
        }

        // /// <summary>
        // /// Обновление прежде сохраненного варианта исходных данных
        // /// </summary>
        // /// <param name="furnace"></param>
        // /// <returns></returns>
        // public async Task<Guid> UpdateFurnaceAsync(FurnaceBaseParam furnace)
        // {
        //     if (furnace != null)
        //     {
        //         var existFurnace = new FurnaceBaseParam();
        //         try
        //         {
        //             existFurnace = await _context.FurnacesWorkParams
        //                 .Include(d => d.MaterialsWorkParamsList)
        //                 .FirstOrDefaultAsync(f => f.Id == furnace.Id);
        //
        //             if (existFurnace != null)
        //             {
        //                 existFurnace.NumberOfFurnace = furnace.NumberOfFurnace;
        //                 existFurnace.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
        //                 existFurnace.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
        //                 existFurnace.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
        //                 existFurnace.DiameterOfRaspar = furnace.DiameterOfRaspar;
        //                 existFurnace.DiameterOfHorn = furnace.DiameterOfHorn;
        //                 existFurnace.HeightOfHorn = furnace.HeightOfHorn;
        //                 existFurnace.HeightOfTuyeres = furnace.HeightOfTuyeres;
        //                 existFurnace.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
        //                 existFurnace.HeightOfRaspar = furnace.HeightOfRaspar;
        //                 existFurnace.HeightOfShaft = furnace.HeightOfShaft;
        //                 existFurnace.HeightOfColoshnik = furnace.HeightOfColoshnik;
        //                 existFurnace.EstablishedLevelOfEmbankment = furnace.EstablishedLevelOfEmbankment;
        //                 existFurnace.NumberOfTuyeres = furnace.NumberOfTuyeres;
        //                 existFurnace.DailyСapacityOfFurnace = furnace.DailyСapacityOfFurnace;
        //                 existFurnace.SpecificConsumptionOfCoke = furnace.SpecificConsumptionOfCoke;
        //                 existFurnace.SpecificConsumptionOfZRM = furnace.SpecificConsumptionOfZRM;
        //                 existFurnace.ShareOfPelletsInCharge = furnace.ShareOfPelletsInCharge;
        //                 existFurnace.BlastConsumption = furnace.BlastConsumption;
        //                 existFurnace.BlastTemperature = furnace.BlastTemperature;
        //                 existFurnace.BlastPressure = furnace.BlastPressure;
        //                 existFurnace.BlastHumidity = furnace.BlastHumidity;
        //                 existFurnace.OxygenContentInBlast = furnace.OxygenContentInBlast;
        //                 existFurnace.NaturalGasConsumption = furnace.NaturalGasConsumption;
        //                 existFurnace.ColoshGasTemperature = furnace.ColoshGasTemperature;
        //                 existFurnace.ColoshGasPressure = furnace.ColoshGasPressure;
        //                 existFurnace.ColoshGas_CO = furnace.ColoshGas_CO;
        //                 existFurnace.ColoshGas_CO2 = furnace.ColoshGas_CO2;
        //                 existFurnace.ColoshGas_H2 = furnace.ColoshGas_H2;
        //                 existFurnace.Chugun_SI = furnace.Chugun_SI;
        //                 existFurnace.Chugun_MN = furnace.Chugun_MN;
        //                 existFurnace.Chugun_P = furnace.Chugun_P;
        //                 existFurnace.Chugun_S = furnace.Chugun_S;
        //                 existFurnace.Chugun_C = furnace.Chugun_C;
        //                 existFurnace.AshContentInCoke = furnace.AshContentInCoke;
        //                 existFurnace.VolatileContentInCoke = furnace.VolatileContentInCoke;
        //                 existFurnace.SulfurContentInCoke = furnace.SulfurContentInCoke;
        //                 existFurnace.SpecificSlagYield = furnace.SpecificSlagYield;
        //                 existFurnace.HeatCapacityOfAgglomerate = furnace.HeatCapacityOfAgglomerate;
        //                 existFurnace.HeatCapacityOfPellets = furnace.HeatCapacityOfPellets;
        //                 existFurnace.HeatCapacityOfCoke = furnace.HeatCapacityOfCoke;
        //                 existFurnace.AcceptedTemperatureOfBackupZone = furnace.AcceptedTemperatureOfBackupZone;
        //                 existFurnace.ProportionOfHeatLossesOfLowerPart = furnace.ProportionOfHeatLossesOfLowerPart;
        //                 existFurnace.AverageSizeOfPieceCharge = furnace.AverageSizeOfPieceCharge;
        //
        //                 existFurnace.HeatOfBurningOfNaturalGasOnFarms = furnace.HeatOfBurningOfNaturalGasOnFarms;
        //                 existFurnace.HeatOfIncompleteBurningCarbonOfCoke = furnace.HeatOfIncompleteBurningCarbonOfCoke;
        //                 existFurnace.TemperatureOfCokeThatCameToTuyeres = furnace.TemperatureOfCokeThatCameToTuyeres;
        //                 existFurnace.Day = furnace.Day;
        //                 existFurnace.SaveDate = DateTime.Now;
        //
        //                 existFurnace.MaterialsWorkParamsList.Clear();
        //                 existFurnace.MaterialsWorkParamsList = furnace.MaterialsWorkParamsList;
        //
        //                 await _context.SaveChangesAsync();
        //
        //                 return existFurnace.Id;
        //             }
        //
        //             return Guid.Empty;
        //         }
        //         catch (Exception ex)
        //         {
        //             Log.Error(
        //                 $"FurnaceService UpdateFurnaceAsync: Ошибка обновления сохраненного варианта исходных данных с идентификатором id = '{furnace.Id}': {ex}");
        //             return Guid.Empty;
        //         }
        //     }
        //
        //     return Guid.Empty;
        // }
        //
        // /// <summary>
        // /// Сохранение нового варианта исходных данных
        // /// </summary>
        // /// <param name="furnace"></param>
        // /// <returns></returns>
        // public async Task<Guid> SaveFurnaceAsync(FurnaceBaseParam furnace, Guid userId)
        // {
        //     if (furnace != null)
        //     {
        //         try
        //         {
        //             furnace.Id = Guid.NewGuid();
        //             furnace.UserId = userId;
        //             furnace.SaveDate = DateTime.Now;
        //             var savedVariant = _context.FurnacesWorkParams.Add(furnace);
        //             await _context.SaveChangesAsync();
        //
        //             return savedVariant.Entity.Id;
        //         }
        //         catch (Exception ex)
        //         {
        //             Log.Error($"HTTP POST api/base PostAsync: Ошибка сохранения варианта исходных данных: {ex}");
        //             return Guid.Empty;
        //         }
        //     }
        //
        //     return Guid.Empty;
        // }
    }
}