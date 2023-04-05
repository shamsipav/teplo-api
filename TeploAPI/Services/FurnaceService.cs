﻿using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Migrations;
using TeploAPI.Models;

namespace TeploAPI.Services
{
    public class FurnaceService
    {
        private TeploDBContext _context;
        public FurnaceService(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Обновление прежде сохраненного варианта исходных данных
        /// </summary>
        /// <param name="furnace"></param>
        /// <returns></returns>
        internal async Task<int> UpdateFurnaceAsync(Furnace furnace)
        {
            if (furnace != null)
            {
                var existFurnace = new Furnace();
                try
                {
                    existFurnace = await _context.Furnaces.FirstOrDefaultAsync(f => f.Id == furnace.Id);

                    if (existFurnace != null)
                    {
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
                        existFurnace.EstablishedLevelOfEmbankment = furnace.EstablishedLevelOfEmbankment;
                        existFurnace.NumberOfTuyeres = furnace.NumberOfTuyeres;
                        existFurnace.DailyСapacityOfFurnace = furnace.DailyСapacityOfFurnace;
                        existFurnace.SpecificConsumptionOfCoke = furnace.SpecificConsumptionOfCoke;
                        existFurnace.SpecificConsumptionOfZRM = furnace.SpecificConsumptionOfZRM;
                        existFurnace.ShareOfPelletsInCharge = furnace.ShareOfPelletsInCharge;
                        existFurnace.BlastConsumption = furnace.BlastConsumption;
                        existFurnace.BlastTemperature = furnace.BlastTemperature;
                        existFurnace.BlastPressure = furnace.BlastPressure;
                        existFurnace.BlastHumidity = furnace.BlastHumidity;
                        existFurnace.OxygenContentInBlast = furnace.OxygenContentInBlast;
                        existFurnace.NaturalGasConsumption = furnace.NaturalGasConsumption;
                        existFurnace.ColoshGasTemperature = furnace.ColoshGasTemperature;
                        existFurnace.ColoshGasPressure = furnace.ColoshGasPressure;
                        existFurnace.ColoshGas_CO = furnace.ColoshGas_CO;
                        existFurnace.ColoshGas_CO2 = furnace.ColoshGas_CO2;
                        existFurnace.ColoshGas_H2 = furnace.ColoshGas_H2;
                        existFurnace.Chugun_SI = furnace.Chugun_SI;
                        existFurnace.Chugun_MN = furnace.Chugun_MN;
                        existFurnace.Chugun_P = furnace.Chugun_P;
                        existFurnace.Chugun_S = furnace.Chugun_S;
                        existFurnace.Chugun_C = furnace.Chugun_C;
                        existFurnace.AshContentInCoke = furnace.AshContentInCoke;
                        existFurnace.VolatileContentInCoke = furnace.VolatileContentInCoke;
                        existFurnace.SulfurContentInCoke = furnace.SulfurContentInCoke;
                        existFurnace.SpecificSlagYield = furnace.SpecificSlagYield;
                        existFurnace.HeatCapacityOfAgglomerate = furnace.HeatCapacityOfAgglomerate;
                        existFurnace.HeatCapacityOfPellets = furnace.HeatCapacityOfPellets;
                        existFurnace.HeatCapacityOfCoke = furnace.HeatCapacityOfCoke;
                        existFurnace.AcceptedTemperatureOfBackupZone = furnace.AcceptedTemperatureOfBackupZone;
                        existFurnace.ProportionOfHeatLossesOfLowerPart = furnace.ProportionOfHeatLossesOfLowerPart;
                        existFurnace.AverageSizeOfPieceCharge = furnace.AverageSizeOfPieceCharge;

                        existFurnace.HeatOfBurningOfNaturalGasOnFarms = furnace.HeatOfBurningOfNaturalGasOnFarms;
                        existFurnace.HeatOfIncompleteBurningCarbonOfCoke = furnace.HeatOfIncompleteBurningCarbonOfCoke;
                        existFurnace.TemperatureOfCokeThatCameToTuyeres = furnace.TemperatureOfCokeThatCameToTuyeres;
                        existFurnace.SaveDate = DateTime.Now;

                        await _context.SaveChangesAsync();

                        return existFurnace.Id;
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    Log.Error($"FurnaceService UpdateFurnaceAsync: Ошибка обновления сохраненного варианта исходных данных с идентификатором id = '{furnace.Id}': {ex}");
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// Сохранение нового варианта исходных данных
        /// </summary>
        /// <param name="furnace"></param>
        /// <returns></returns>
        internal async Task<int> SaveFurnaceAsync(Furnace furnace, int userId)
        {
            if (furnace != null)
            {
                try
                {
                    furnace.UserId = userId;
                    furnace.SaveDate = DateTime.Now;
                    _context.Furnaces.Add(furnace);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/base PostAsync: Ошибка сохранения варианта исходных данных: {ex}");
                    return 0;
                }
            }

            return 0;
        }
    }
}