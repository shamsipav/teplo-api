using TeploAPI.Models;

namespace TeploAPI.Services
{
    /// <summary>
    /// Сервис для проведения расчетов
    /// </summary>
    public class CalculateService
    {
        public Result СalculateThermalRegime(Furnace input)
        {
            var result = new Result();

            // ПРЕДВАРИТЕЛЬНЫЕ РАСЧЕТЫ
            result.AverageSectionalAreaOfFurnaceShaft = Math.PI * 0.25 * Math.Pow(((0.001 * input.DiameterOfColoshnik + 0.001 * input.DiameterOfRaspar) * 0.5), 2);

            result.HeatCapacityOfDiatomicGasesAtBlastTemperature = 1.2897 + 0.000121 * input.BlastTemperature;
            result.HeatCapacityOfWaterVaporAtBlastTemperature = 1.456 + 0.000282 * input.BlastTemperature;
            result.DegreeOfDirectRecovery = 0.54 - 0.00214 * input.NaturalGasConsumption;
            result.AmountOfCokeCarbonCameIntoFurnace = input.SpecificConsumptionOfCoke * 0.01 * (100 - input.AshContentInCoke - input.VolatileContentInCoke - input.SulfurContentInCoke);

            result.CarbonConsumptionForIronReduction = 940 * result.DegreeOfDirectRecovery * 12 / 56;
            result.CarbonСonsumptionForCastChugunReduction = 10 * (input.Chugun_MN * 0.218 + input.Chugun_P * 0.968 + input.Chugun_SI * 0.857 + input.Chugun_S * 0.375);
            result.CarbonConsumptionForMethaneFormation = 0.008 * result.AmountOfCokeCarbonCameIntoFurnace;
            result.AmountOfCarbonOfCokeBurningAtTuyeres = result.AmountOfCokeCarbonCameIntoFurnace - result.CarbonConsumptionForIronReduction - result.CarbonСonsumptionForCastChugunReduction - result.CarbonConsumptionForMethaneFormation - 10 * input.Chugun_C;

            result.ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke = input.NaturalGasConsumption / result.AmountOfCarbonOfCokeBurningAtTuyeres;
            result.ConsumptionOfNaturalGasPerOneKgOfOfCoke = 0.9333 / (0.01 * input.OxygenContentInBlast + 0.00062 * input.BlastHumidity);
            result.BlastConsumptionForBurningOneMeterCoubOfNaturalGas = 0.5 / (0.01 * input.OxygenContentInBlast + 0.00062 * input.BlastHumidity);
            result.SpecificBlastConsumption = (result.ConsumptionOfNaturalGasPerOneKgOfOfCoke + result.ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke * result.BlastConsumptionForBurningOneMeterCoubOfNaturalGas) * result.AmountOfCarbonOfCokeBurningAtTuyeres;

            result.AmountOfGorenjeGasesDuringCombustionOneKgOfCarbonCoke = 1.8667 + result.ConsumptionOfNaturalGasPerOneKgOfOfCoke * (1 - 0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.NumberOfFurnaceGasesAtConversionOfOneMeterCoubOfGas = 3 + result.BlastConsumptionForBurningOneMeterCoubOfNaturalGas * (1 - 0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.FurnaceGasOutput = (result.AmountOfGorenjeGasesDuringCombustionOneKgOfCarbonCoke + result.ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke * result.NumberOfFurnaceGasesAtConversionOfOneMeterCoubOfGas) * result.AmountOfCarbonOfCokeBurningAtTuyeres;

            result.GasFiltrationFurnaceShaftSpeed = (((result.FurnaceGasOutput + 376 * result.DegreeOfDirectRecovery) * input.DailyСapacityOfFurnace) / 1440) / (result.AverageSectionalAreaOfFurnaceShaft * 60);

            result.AmountOfHeatFromGorenjeCoke = result.AmountOfCarbonOfCokeBurningAtTuyeres * 9.8;
            result.HeatOfHeatedBlast = 0.001 * result.SpecificBlastConsumption * (result.HeatCapacityOfDiatomicGasesAtBlastTemperature * (1 - 0.00124 * input.BlastHumidity) + 0.00124 * input.BlastHumidity * result.HeatCapacityOfWaterVaporAtBlastTemperature) * input.BlastTemperature;
            result.HeatOfNaturalGasConversion = 0.001 * input.NaturalGasConsumption * 1657;
            result.HeatCapacityOfGasAtTemperatureOfReserveZone = 1.2897 + 0.000121 * input.AcceptedTemperatureOfBackupZone;

            result.UsefulArrivalOfHeatInLowerZoneOfTheFurnace = 1000 * (result.AmountOfHeatFromGorenjeCoke + result.HeatOfHeatedBlast + result.HeatOfNaturalGasConversion - (0.001 * result.FurnaceGasOutput * result.HeatCapacityOfGasAtTemperatureOfReserveZone * input.AcceptedTemperatureOfBackupZone + 940 * 22.4 * result.DegreeOfDirectRecovery / 56));
            result.AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge = (input.SpecificConsumptionOfZRM * (1 - input.ShareOfPelletsInCharge) * input.HeatCapacityOfAgglomerate + input.SpecificConsumptionOfZRM * input.ShareOfPelletsInCharge * input.HeatCapacityOfPellets + input.SpecificConsumptionOfCoke * input.HeatCapacityOfCoke) * input.AcceptedTemperatureOfBackupZone;
            result.HeatCostsForDirectIronRecovery = 31750 * 94 * result.DegreeOfDirectRecovery;
            result.HeatLossesToEnvironmentThrough = ((result.AmountOfHeatFromGorenjeCoke + result.HeatOfHeatedBlast + result.HeatOfNaturalGasConversion) * input.ProportionOfHeatLossesOfLowerPart) * 1000;
            result.CalculatedGeneralizingParameter = result.UsefulArrivalOfHeatInLowerZoneOfTheFurnace + result.AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge - result.HeatCostsForDirectIronRecovery - result.HeatLossesToEnvironmentThrough;
            result.OptimalHeatConsumptionForSmeltingOneTonOfCastIron = 265500 * input.Chugun_SI + 52250 * input.Chugun_MN + 263000 * input.Chugun_P + 598000 + input.SpecificSlagYield * 0.001 * input.Chugun_S + 1000 * (0.9 * 1450 + 1.26 * 1500 * 0.001 * input.SpecificSlagYield);

            result.IndexOfTheBottomOfTheFurnace = result.CalculatedGeneralizingParameter / result.OptimalHeatConsumptionForSmeltingOneTonOfCastIron;
            result.ValueOfHeatLossesInLowerZoneOfFurnace = (result.UsefulArrivalOfHeatInLowerZoneOfTheFurnace + result.AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge - result.HeatCostsForDirectIronRecovery - result.OptimalHeatConsumptionForSmeltingOneTonOfCastIron) / ((result.AmountOfHeatFromGorenjeCoke + result.HeatOfHeatedBlast + result.HeatOfNaturalGasConversion) * 1000);

            result.TrueHeatCapacityOfGrateGas = 1.283 + 0.000214 * input.ColoshGasTemperature + (4.3 + 0.0073 * input.ColoshGasTemperature) * input.ColoshGas_CO2 * 0.001;
            result.ThermalConductivityOfGrateGas = (19.4 + 1.826 * input.ColoshGas_H2 + 0.0073 * input.ColoshGasTemperature) * 0.001;
            result.KinematicViscosityOfGrateGas = (1.456 * input.ColoshGasTemperature + 5.14 * input.ColoshGas_H2 - 35.43) * 0.0000001;
            result.VolumetricHeatTransferCoefficient = ((0.259 * Math.Pow(result.TrueHeatCapacityOfGrateGas, 0.333) * Math.Pow(result.ThermalConductivityOfGrateGas, 0.6667) * Math.Pow(result.GasFiltrationFurnaceShaftSpeed, 0.9) * Math.Pow((1 + input.ColoshGasTemperature / 273), 0.57)) / (Math.Pow(input.AverageSizeOfPieceCharge, 1.1) * Math.Pow(result.KinematicViscosityOfGrateGas, 0.57))) * 0.001;
            // TODO: Под сомнением выше
            result.ChargeFlowRatePassingThroughFurnaceShaft = (input.SpecificConsumptionOfCoke + input.SpecificConsumptionOfZRM) * input.DailyСapacityOfFurnace / (24 * 60 * 60);
            result.AverageHeatCapacityOfCharge = (1 - input.ShareOfPelletsInCharge) * input.HeatCapacityOfAgglomerate * 0.25 + input.ShareOfPelletsInCharge * input.HeatCapacityOfPellets * 0.25 + 0.5 * input.HeatCapacityOfCoke;

            // TODO: Ниже всё нужно перепроверить
            result.IntermediateNumerator = result.ChargeFlowRatePassingThroughFurnaceShaft * result.AverageHeatCapacityOfCharge * Math.Pow((input.AcceptedTemperatureOfBackupZone - 100), 2);
            result.IntermediateDenominator = result.VolumetricHeatTransferCoefficient * result.AverageSectionalAreaOfFurnaceShaft * 7 * input.AcceptedTemperatureOfBackupZone * (input.ColoshGasTemperature - 100);
            result.IntermediateRatio = result.IntermediateNumerator / result.IntermediateDenominator;
            result.IntermediateExhibitor = -(result.VolumetricHeatTransferCoefficient * result.AverageSectionalAreaOfFurnaceShaft * 7 * (input.ColoshGasTemperature - 100)) / (result.ChargeFlowRatePassingThroughFurnaceShaft * result.AverageHeatCapacityOfCharge * (input.AcceptedTemperatureOfBackupZone - 100));

            result.IndexOfTheFurnaceTop = 1 - result.IntermediateRatio * (1 - Math.Pow(2.7183, result.IntermediateExhibitor));

            return result;
        }
    }
}
