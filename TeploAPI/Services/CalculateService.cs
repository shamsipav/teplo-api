using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Services
{
    // TODO: Перепроверить расчеты
    /// <summary>
    /// Сервис для проведения расчетов
    /// </summary>
    public class CalculateService : ICalculateService
    {
        public Result СalculateThermalRegime(FurnaceBaseParam input)
        {
            var result = new Result();

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
            result.ChargeFlowRatePassingThroughFurnaceShaft = (input.SpecificConsumptionOfCoke + input.SpecificConsumptionOfZRM) * input.DailyСapacityOfFurnace / (24 * 60 * 60);
            result.AverageHeatCapacityOfCharge = (1 - input.ShareOfPelletsInCharge) * input.HeatCapacityOfAgglomerate * 0.25 + input.ShareOfPelletsInCharge * input.HeatCapacityOfPellets * 0.25 + 0.5 * input.HeatCapacityOfCoke;

            result.IntermediateNumerator = result.ChargeFlowRatePassingThroughFurnaceShaft * result.AverageHeatCapacityOfCharge * Math.Pow((input.AcceptedTemperatureOfBackupZone - 100), 2);
            result.IntermediateDenominator = result.VolumetricHeatTransferCoefficient * result.AverageSectionalAreaOfFurnaceShaft * 7 * input.AcceptedTemperatureOfBackupZone * (input.ColoshGasTemperature - 100);
            result.IntermediateRatio = result.IntermediateNumerator / result.IntermediateDenominator;
            result.IntermediateExhibitor = -(result.VolumetricHeatTransferCoefficient * result.AverageSectionalAreaOfFurnaceShaft * 7 * (input.ColoshGasTemperature - 100)) / (result.ChargeFlowRatePassingThroughFurnaceShaft * result.AverageHeatCapacityOfCharge * (input.AcceptedTemperatureOfBackupZone - 100));

            result.IndexOfTheFurnaceTop = 1 - result.IntermediateRatio * (1 - Math.Pow(2.7183, result.IntermediateExhibitor));

            result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke = (0.9333) / (0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas = (0.5) / (0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.OutputOfTheTuyereGasBurningAtTheTuyeres = 1.8667 + result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke * (1 - 0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.OutputOfTuyereGasOfNaturalGasDuringСonversion = 3 + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas * (1 - 0.01 * input.OxygenContentInBlast + 0.00124 * input.BlastHumidity);
            result.HeatCapacityOfDiatomicGasesAtHotBlastTemperature = 1.2897 + 0.000121 * input.BlastTemperature;
            result.HeatCapacityOfWaterVaporAtHotBlastTemperature = 1.4560 + 0.000282 * input.BlastTemperature;
            result.HeatContentOfHotBlast = result.HeatCapacityOfDiatomicGasesAtHotBlastTemperature * input.BlastTemperature - 0.00124 * input.BlastHumidity * (10800 - result.HeatCapacityOfWaterVaporAtHotBlastTemperature * input.BlastTemperature);
            result.HeatContentOfCarbonOfCokeToTuyeres = input.HeatCapacityOfCoke * input.TemperatureOfCokeThatCameToTuyeres;
            result.NaturalGasConsumptionPerOneKgOfCoke = input.NaturalGasConsumption / result.AmountOfCarbonOfCokeBurningAtTuyeres;
            result.HeatContentOfFurnaceGases = (input.HeatOfIncompleteBurningCarbonOfCoke + result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke * result.HeatContentOfHotBlast + result.HeatContentOfCarbonOfCokeToTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * (input.HeatOfBurningOfNaturalGasOnFarms + result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas * result.HeatContentOfHotBlast)) / (result.OutputOfTheTuyereGasBurningAtTheTuyeres + result.NaturalGasConsumptionPerOneKgOfCoke * result.OutputOfTuyereGasOfNaturalGasDuringСonversion);
            result.TheoreticalBurningTemperatureOfCarbonCoke = 165 + 0.6113 * result.HeatContentOfFurnaceGases;

            return result;
        }

        // TODO: Refactoring.
        public ProjectDataViewModel CalculateProjectThermalRegime(FurnaceBaseParam input, FurnaceProjectParam project, Reference reference)
        {
            double cokeConsumption = input.SpecificConsumptionOfCoke;
            double furnanceCapacity = input.DailyСapacityOfFurnace;

            double changeCokeConsumption = 0;
            double changeFurnanceCapacity = 0;

            double blastTemperatureDifference = (project.BlastTemperature - input.BlastTemperature) / 10;

            if (input.BlastTemperature >= 800 && project.BlastTemperature <= 900)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf800to900 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf800to900 * blastTemperatureDifference / 100);
            }
            else if (input.BlastTemperature >= 901 && project.BlastTemperature <= 1000)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf901to1000 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf901to1000 * blastTemperatureDifference / 100);
            }
            else if (input.BlastTemperature >= 1001 && project.BlastTemperature <= 1100)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1001to1100 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1001to1100 * blastTemperatureDifference / 100);
            }
            else if (input.BlastTemperature >= 1101 && project.BlastTemperature <= 1200)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1101to1200 * blastTemperatureDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1101to1200 * blastTemperatureDifference / 100);
            }

            double blastHumidityDifference = project.BlastHumidity - input.BlastHumidity;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseBlastHumidity * blastHumidityDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseBlastHumidity * blastHumidityDifference / 100);

            double oxygenContentInBlastDifference = project.OxygenContentInBlast - input.OxygenContentInBlast;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseVolumeFractionOxygenInBlast * oxygenContentInBlastDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseVolumeFractionOxygenInBlast * oxygenContentInBlastDifference / 100);

            double naturalGasConsumptionDifference = project.NaturalGasConsumption - input.NaturalGasConsumption;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseNaturalGasCunsimption * naturalGasConsumptionDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseNaturalGasCunsimption * naturalGasConsumptionDifference / 100);

            double coloshGasPressureDifference = project.ColoshGasPressure - input.ColoshGasPressure;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseGasPressureUnderGrate * coloshGasPressureDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseGasPressureUnderGrate * coloshGasPressureDifference / 100);

            double chugun_SIdifference = (project.Chugun_SI - input.Chugun_SI) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.ReductionMassFractionOfSiliciumInChugun * chugun_SIdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.ReductionMassFractionOfSiliciumInChugun * chugun_SIdifference / 100);

            double chugun_MNdifference = (project.Chugun_MN - input.Chugun_MN) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseMassFractionOfManganeseInChugun * chugun_MNdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseMassFractionOfManganeseInChugun * chugun_MNdifference / 100);

            double chugun_Pdifference = (project.Chugun_P - input.Chugun_P) / 0.1;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.IncreaseMassFractionOfPhosphorusInChugun * chugun_Pdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.IncreaseMassFractionOfPhosphorusInChugun * chugun_Pdifference / 100);

            double chugun_Sdifference = (project.Chugun_S - input.Chugun_S) / 0.01;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.ReductionMassFractionOfSeraInChugun * chugun_Sdifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.ReductionMassFractionOfSeraInChugun * chugun_Sdifference / 100);

            double ashContentDifference = project.AshContentInCoke - input.AshContentInCoke;

            if (input.AshContentInCoke >= 11 && project.AshContentInCoke <= 12)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent * ashContentDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent * ashContentDifference / 100);
            }
            else if (input.AshContentInCoke >= 12 && project.AshContentInCoke <= 13)
            {
                changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent * ashContentDifference / 100);
                changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent * ashContentDifference / 100);
            }

            double sulfurContentInCokeDifference = (project.SulfurContentInCoke - input.SulfurContentInCoke) / 0.01;
            changeCokeConsumption += (cokeConsumption * reference.CokeCunsumptionReference.ReductionMassFractionOfSera * sulfurContentInCokeDifference / 100);
            changeFurnanceCapacity += (furnanceCapacity * reference.FurnaceCapacityReference.ReductionMassFractionOfSera * sulfurContentInCokeDifference / 100);

            input.SpecificConsumptionOfCoke += changeCokeConsumption;
            input.DailyСapacityOfFurnace += changeFurnanceCapacity;

            return new ProjectDataViewModel { ChangedInputData = input, ProjectInputData = project };
        }
    }
}