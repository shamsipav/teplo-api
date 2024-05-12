using TeploMath;

namespace TeploMathTest;

public class Tests
{
    private static Result _result;

    [SetUp]
    public void Setup()
    {
        InputData inputData = new InputData()
        {
            NumberOfFurnace = 1,
            UsefulVolumeOfFurnace = 1370,
            UsefulHeightOfFurnace = 26805,
            DiameterOfColoshnik = 6600,
            DiameterOfRaspar = 9500,
            DiameterOfHorn = 8400,
            HeightOfHorn = 4000,
            HeightOfTuyeres = 3600,
            HeightOfZaplechiks = 2800,
            HeightOfRaspar = 2000,
            HeightOfShaft = 15250,
            HeightOfColoshnik = 1900,
            EstablishedLevelOfEmbankment = 1000,
            NumberOfTuyeres = 20,
            DailyСapacityOfFurnace = 3822.55,
            SpecificConsumptionOfCoke = 405.6,
            SpecificConsumptionOfZRM = 1656.5,
            ShareOfPelletsInCharge = 0.3567,
            BlastConsumption = 2533.3,
            BlastTemperature = 1250,
            BlastPressure = 2.85,
            BlastHumidity = 1.9,
            OxygenContentInBlast = 28.55,
            NaturalGasConsumption = 141.5,
            ColoshGasTemperature = 200,
            ColoshGasPressure = 1.45,
            ColoshGas_CO = 23.7,
            ColoshGas_CO2 = 18.84,
            ColoshGas_H2 = 10.11,
            Chugun_SI = 0.562,
            Chugun_MN = 0.268,
            Chugun_P = 0.057,
            Chugun_S = 0.018,
            Chugun_C = 4.846,
            AshContentInCoke = 11.53,
            VolatileContentInCoke = 0.72,
            SulfurContentInCoke = 0.47,
            SpecificSlagYield = 350.08,
            HeatCapacityOfAgglomerate = 0.75,
            HeatCapacityOfPellets = 0.8,
            HeatCapacityOfCoke = 1.09,
            AcceptedTemperatureOfBackupZone = 950,
            ProportionOfHeatLossesOfLowerPart = 0,
            AverageSizeOfPieceCharge = 0.018,
            HeatOfBurningOfNaturalGasOnFarms = 1590,
            HeatOfIncompleteBurningCarbonOfCoke = 9800,
            TemperatureOfCokeThatCameToTuyeres = 1500
        };

        ThermalRegime thermalRegime = new ThermalRegime();

        _result = thermalRegime.CalculateThermalRegime(inputData);
    }

    [Test]
    public void AverageSectionalAreaOfFurnaceShaft()
    {
        double expected = 50.896;
        Assert.AreEqual(expected, Math.Round(_result.AverageSectionalAreaOfFurnaceShaft, 3));
    }

    [Test]
    public void GasFiltrationFurnaceShaftSpeed()
    {
        double expected = 1.509;
        Assert.AreEqual(expected, Math.Round(_result.GasFiltrationFurnaceShaftSpeed, 3));
    }

    [Test]
    public void HeatCapacityOfDiatomicGasesAtBlastTemperature()
    {
        double expected = 1.441;
        Assert.AreEqual(expected, Math.Round(_result.HeatCapacityOfDiatomicGasesAtBlastTemperature, 3));
    }

    [Test]
    public void HeatCapacityOfWaterVaporAtBlastTemperature()
    {
        double expected = 1.808;
        Assert.AreEqual(expected, Math.Round(_result.HeatCapacityOfWaterVaporAtBlastTemperature, 3));
    }

    [Test]
    public void DegreeOfDirectRecovery()
    {
        double expected = 0.237;
        Assert.AreEqual(expected, Math.Round(_result.DegreeOfDirectRecovery, 3));
    }

    [Test]
    public void AmountOfCokeCarbonCameIntoFurnace()
    {
        double expected = 354.008;
        Assert.AreEqual(expected, Math.Round(_result.AmountOfCokeCarbonCameIntoFurnace, 3));
    }

    [Test]
    public void CarbonConsumptionForIronReduction()
    {
        double expected = 47.777;
        Assert.AreEqual(expected, Math.Round(_result.CarbonConsumptionForIronReduction, 3));
    }

    [Test]
    public void CarbonСonsumptionForCastChugunReduction()
    {
        double expected = 6.02;
        Assert.AreEqual(expected, Math.Round(_result.CarbonСonsumptionForCastChugunReduction, 2));
    }

    [Test]
    public void CarbonConsumptionForMethaneFormation()
    {
        double expected = 2.832;
        Assert.AreEqual(expected, Math.Round(_result.CarbonConsumptionForMethaneFormation, 3));
    }

    [Test]
    public void AmountOfCarbonOfCokeBurningAtTuyeres()
    {
        double expected = 248.919;
        Assert.AreEqual(expected, Math.Round(_result.AmountOfCarbonOfCokeBurningAtTuyeres, 3));
    }

    [Test]
    public void ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke()
    {
        double expected = 0.568;
        Assert.AreEqual(expected, Math.Round(_result.ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke, 3));
    }

    [Test]
    public void ConsumptionOfNaturalGasPerOneKgOfOfCoke()
    {
        double expected = 3.256;
        Assert.AreEqual(expected, Math.Round(_result.ConsumptionOfNaturalGasPerOneKgOfOfCoke, 3));
    }

    [Test]
    public void BlastConsumptionForBurningOneMeterCoubOfNaturalGas()
    {
        double expected = 1.744;
        Assert.AreEqual(expected, Math.Round(_result.BlastConsumptionForBurningOneMeterCoubOfNaturalGas, 3));
    }

    [Test]
    public void SpecificBlastConsumption()
    {
        double expected = 1057.165;
        Assert.AreEqual(expected, Math.Round(_result.SpecificBlastConsumption, 3));
    }

    [Test]
    public void AmountOfGorenjeGasesDuringCombustionOneKgOfCarbonCoke()
    {
        double expected = 4.200;
        Assert.AreEqual(expected, Math.Round(_result.AmountOfGorenjeGasesDuringCombustionOneKgOfCarbonCoke, 3));
    }

    [Test]
    public void NumberOfFurnaceGasesAtConversionOfOneMeterCoubOfGas()
    {
        double expected = 4.250;
        Assert.AreEqual(expected, Math.Round(_result.NumberOfFurnaceGasesAtConversionOfOneMeterCoubOfGas, 3));
    }

    [Test]
    public void FurnaceGasOutput()
    {
        double expected = 1646.992;
        Assert.AreEqual(expected, Math.Round(_result.FurnaceGasOutput, 3));
    }

    [Test]
    public void AmountOfHeatFromGorenjeCoke()
    {
        double expected = 2439.406;
        Assert.AreEqual(expected, Math.Round(_result.AmountOfHeatFromGorenjeCoke, 3));
    }

    [Test]
    public void HeatOfHeatedBlast()
    {
        double expected = 1905.297;
        Assert.AreEqual(expected, Math.Round(_result.HeatOfHeatedBlast, 3));
    }

    [Test]
    public void HeatOfNaturalGasConversion()
    {
        double expected = 234.466;
        Assert.AreEqual(expected, Math.Round(_result.HeatOfNaturalGasConversion, 3));
    }

    [Test]
    public void HeatCapacityOfGasAtTemperatureOfReserveZone()
    {
        double expected = 1.405;
        Assert.AreEqual(expected, Math.Round(_result.HeatCapacityOfGasAtTemperatureOfReserveZone, 3));
    }

    [Test]
    public void UsefulArrivalOfHeatInLowerZoneOfTheFurnace()
    {
        double expected = 2292209.591;
        Assert.AreEqual(expected, Math.Round(_result.UsefulArrivalOfHeatInLowerZoneOfTheFurnace, 3));
    }

    [Test]
    public void AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge()
    {
        double expected = 1628321.544;
        Assert.AreEqual(expected, Math.Round(_result.AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge, 3));
    }

    [Test]
    public void HeatCostsForDirectIronRecovery()
    {
        double expected = 707893.560;
        Assert.AreEqual(expected, Math.Round(_result.HeatCostsForDirectIronRecovery, 2));
    }

    [Test]
    public void HeatLossesToEnvironmentThrough()
    {
        double expected = 0;
        Assert.AreEqual(expected, Math.Round(_result.HeatLossesToEnvironmentThrough, 2));
    }

    [Test]
    public void CalculatedGlizingParameter()
    {
        double expected = 3212637.579;
        Assert.AreEqual(expected, Math.Round(_result.CalculatedGeneralizingParameter, 3));
    }

    [Test]
    public void OptimalHeatConsumptionForSmeltingOneTonOfCastIron()
    {
        double expected = 2742856.206;
        Assert.AreEqual(expected, Math.Round(_result.OptimalHeatConsumptionForSmeltingOneTonOfCastIron, 3));
    }

    [Test]
    public void IndexOfTheBottomOfTheFurnace()
    {
        double expected = 1.171;
        Assert.AreEqual(expected, Math.Round(_result.IndexOfTheBottomOfTheFurnace, 3));
    }

    [Test]
    public void ValueOfHeatLossesInLowerZoneOfFurnace()
    {
        double expected = 0.103;
        Assert.AreEqual(expected, Math.Round(_result.ValueOfHeatLossesInLowerZoneOfFurnace, 3));
    }

    [Test]
    public void TrueHeatCapacityOfGrateGas()
    {
        double expected = 1.434;
        Assert.AreEqual(expected, Math.Round(_result.TrueHeatCapacityOfGrateGas, 3));
    }

    [Test]
    public void ThermalConductivityOfGrateGas()
    {
        double expected = 0.039;
        Assert.AreEqual(expected, Math.Round(_result.ThermalConductivityOfGrateGas, 3));
    }

    [Test]
    public void KinematicViscosityOfGrateGas()
    {
        double expected = 0.00003;
        Assert.AreEqual(expected, Math.Round(_result.KinematicViscosityOfGrateGas, 5));
    }

    [Test]
    public void VolumetricHeatTransferCoefficient()
    {
        double expected = 2.072;
        Assert.AreEqual(expected, Math.Round(_result.VolumetricHeatTransferCoefficient, 3));
    }

    [Test]
    public void ChargeFlowRatePassingThroughFurnaceShaft()
    {
        double expected = 91.232;
        Assert.AreEqual(expected, Math.Round(_result.ChargeFlowRatePassingThroughFurnaceShaft, 3));
    }

    [Test]
    public void AverageHeatCapacityOfCharge()
    {
        double expected = 0.737;
        Assert.AreEqual(expected, Math.Round(_result.AverageHeatCapacityOfCharge, 3));
    }

    [Test]
    public void IntermediateNumerator()
    {
        double expected = 48576943.554;
        Assert.AreEqual(expected, Math.Round(_result.IntermediateNumerator, 3));
    }

    [Test]
    public void IntermediateDenominator()
    {
        double expected = 70125301.899;
        Assert.AreEqual(expected, Math.Round(_result.IntermediateDenominator, 3));
    }

    [Test]
    public void IntermediateRatio()
    {
        double expected = 0.693;
        Assert.AreEqual(expected, Math.Round(_result.IntermediateRatio, 3));
    }
    
    [Test]
    public void IntermediateExhibitor()
    {
        double expected = -1.292;
        Assert.AreEqual(expected, Math.Round(_result.IntermediateExhibitor, 3));
    }

    [Test]
    public void IndexOfTheFurnaceTop()
    {
        double expected = 0.498;
        Assert.AreEqual(expected, Math.Round(_result.IndexOfTheFurnaceTop, 3));
    }

    [Test]
    public void BlastConsumptionRequiredForBurningOneKgOfCarbonCoke()
    {
        double expected = 3.242;
        Assert.AreEqual(expected, Math.Round(_result.BlastConsumptionRequiredForBurningOneKgOfCarbonCoke, 3));
    }

    [Test]
    public void BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas()
    {
        double expected = 1.737;
        Assert.AreEqual(expected, Math.Round(_result.BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas, 3));
    }

    [Test]
    public void OutputOfTheTuyereGasBurningAtTheTuyeres()
    {
        double expected = 4.191;
        Assert.AreEqual(expected, Math.Round(_result.OutputOfTheTuyereGasBurningAtTheTuyeres, 3));
    }

    [Test]
    public void OutputOfTuyereGasOfNaturalGasDuringСonversion()
    {
        double expected = 4.245;
        Assert.AreEqual(expected, Math.Round(_result.OutputOfTuyereGasOfNaturalGasDuringСonversion, 3));
    }

    [Test]
    public void HeatCapacityOfDiatomicGasesAtHotBlastTemperature()
    {
        double expected = 1.441;
        Assert.AreEqual(expected, Math.Round(_result.HeatCapacityOfDiatomicGasesAtHotBlastTemperature, 3));
    }

    [Test]
    public void HeatCapacityOfWaterVaporAtHotBlastTemperature()
    {
        double expected = 1.808;
        Assert.AreEqual(expected, Math.Round(_result.HeatCapacityOfWaterVaporAtHotBlastTemperature, 3));
    }

    [Test]
    public void HeatContentOfHotBlast()
    {
        double expected = 1781.069;
        Assert.AreEqual(expected, Math.Round(_result.HeatContentOfHotBlast, 3));
    }

    [Test]
    public void HeatContentOfCarbonOfCokeToTuyeres()
    {
        double expected = 1635.000;
        Assert.AreEqual(expected, Math.Round(_result.HeatContentOfCarbonOfCokeToTuyeres, 3));
    }

    [Test]
    public void NaturalGasConsumptionPerOneKgOfCoke()
    {
        double expected = 0.568;
        Assert.AreEqual(expected, Math.Round(_result.NaturalGasConsumptionPerOneKgOfCoke, 3));
    }

    [Test]
    public void HeatContentOfFurnaceGases()
    {
        double expected = 3009.051;
        Assert.AreEqual(expected, Math.Round(_result.HeatContentOfFurnaceGases, 3));
    }

    [Test]
    public void TheoreticalBurningTemperatureOfCarbonCoke()
    {
        double expected = 2004.433;
        Assert.AreEqual(expected, Math.Round(_result.TheoreticalBurningTemperatureOfCarbonCoke, 3));
    }
}