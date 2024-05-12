namespace TeploMath;

/// <summary>
/// Расчетные поля теплового режима
/// </summary>
public class Result
{
    /// <summary>
    /// Средняя площадь сечения шахты печи, м2
    /// </summary>
    public double AverageSectionalAreaOfFurnaceShaft { get; set; }

    /// <summary>
    /// Скорость фильтрации газа через шахту печи, м/с
    /// </summary>
    public double GasFiltrationFurnaceShaftSpeed { get; set; }

    /// <summary>
    /// Теплоемкость двуатомных газов при температуре дутья, кДж/(м3*С)
    /// </summary>
    public double HeatCapacityOfDiatomicGasesAtBlastTemperature { get; set; }

    /// <summary>
    /// Теплоемкость паров воды при температур дутья, кДж/(м3*С)
    /// </summary>
    public double HeatCapacityOfWaterVaporAtBlastTemperature { get; set; }

    /// <summary>
    /// Степень прямого восстановления, доли ед.
    /// </summary>
    public double DegreeOfDirectRecovery { get; set; }

    /// <summary>
    /// Количество пришедшего в печь углерода кокса, кг/т чугуна
    /// </summary>
    public double AmountOfCokeCarbonCameIntoFurnace { get; set; }

    /// <summary>
    /// Расход углерода на прямое восстановление железа, кг/т чугуна
    /// </summary>
    public double CarbonConsumptionForIronReduction { get; set; }

    /// <summary>
    /// Расход углерода на восстановление примесей чугуна, кг/т чугуна
    /// </summary>
    public double CarbonСonsumptionForCastChugunReduction { get; set; }

    /// <summary>
    /// Расход углерода на образование метана, кг/т чугуна
    /// </summary>
    public double CarbonConsumptionForMethaneFormation { get; set; }

    /// <summary>
    /// Количество углерода кокса, сгорающего у фурм (Сф), кг/т чугуна
    /// </summary>
    public double AmountOfCarbonOfCokeBurningAtTuyeres { get; set; }

    /// <summary>
    /// Расход природного газа на 1 кг углерода кокса, сгорающего у фурм, м3/кг*Сф
    /// </summary>
    public double ConsumptionOfNaturalGasPerOneKgOfCarbonOfCoke { get; set; }

    /// <summary>
    /// Расход дутья на сжигание 1 кг кокса, сгорающего у фурм, м3/кг*Сф
    /// </summary>
    public double ConsumptionOfNaturalGasPerOneKgOfOfCoke { get; set; }

    /// <summary>
    /// Расход дутья на сжигание 1 м3 природного газа, м3/м3
    /// </summary>
    public double BlastConsumptionForBurningOneMeterCoubOfNaturalGas { get; set; }

    /// <summary>
    /// Удельный расход дутья, м3/т чугуна
    /// </summary>
    public double SpecificBlastConsumption { get; set; }

    /// <summary>
    /// Количество горновых газов при горении 1 кг углерода кокса, сгорающего у фурм, м3/кг Сф
    /// </summary>
    public double AmountOfGorenjeGasesDuringCombustionOneKgOfCarbonCoke { get; set; }

    /// <summary>
    /// Количество горновых газов при конверсии 1 м3 газа, м3/м3
    /// </summary>
    public double NumberOfFurnaceGasesAtConversionOfOneMeterCoubOfGas { get; set; }

    /// <summary>
    /// Выход горнового газа, м3/т чугуна
    /// </summary>
    public double FurnaceGasOutput { get; set; }

    /// <summary>
    /// Количество тепла от горения кокса, кДж/кг чугуна
    /// </summary>
    public double AmountOfHeatFromGorenjeCoke { get; set; }

    /// <summary>
    /// Тепло нагретого дутья, кДж/кг чугуна
    /// </summary>
    public double HeatOfHeatedBlast { get; set; }

    /// <summary>
    /// Тепло конверсии природного газаа, кДж/кг чугуна
    /// </summary>
    public double HeatOfNaturalGasConversion { get; set; }

    /// <summary>
    /// Теплоемкость газа при температуре "резервной зоны", кДж/(м3*С)
    /// </summary>
    public double HeatCapacityOfGasAtTemperatureOfReserveZone { get; set; }

    /// <summary>
    /// "Полезный" приход тепла в нижнюю зону печи, кДж/т чугуна
    /// </summary>
    public double UsefulArrivalOfHeatInLowerZoneOfTheFurnace { get; set; }

    /// <summary>
    /// Количество тепла, поступающего в нижнюю зону печи с шихтой при температуре "резервной зоны", кДж/т чугуна
    /// </summary>
    public double AmountOfHeatEnteringLlowerZoneOfFurnaceWithCharge { get; set; }

    /// <summary>
    /// Затраты тепла на прямое восстановление железа, кДж/т чугуна
    /// </summary>
    public double HeatCostsForDirectIronRecovery { get; set; }

    /// <summary>
    /// Тепловые потери в окружающюю среду через фурменный пояс, распар и заплечики, кДж/т чугуна
    /// </summary>
    public double HeatLossesToEnvironmentThrough { get; set; }

    /// <summary>
    /// Расчетный обобщающий параметр (Qp), [-]
    /// </summary>
    public double CalculatedGeneralizingParameter { get; set; }

    /// <summary>
    /// Оптимальные затраты тепла на выплавку 1 тонны чугуна, кДж/т чугуна
    /// </summary>
    public double OptimalHeatConsumptionForSmeltingOneTonOfCastIron { get; set; }

    /// <summary>
    /// ИНДЕКС НИЗА ПЕЧИ
    /// </summary>
    public double IndexOfTheBottomOfTheFurnace { get; set; }

    /// <summary>
    /// Значение доли тепловых потерь в нижней зоне печи при условии i=1,0
    /// </summary>
    public double ValueOfHeatLossesInLowerZoneOfFurnace { get; set; }

    /// <summary>
    /// Истинная теплоемкость колошникового газа, кДж/(м3*С)
    /// </summary>
    public double TrueHeatCapacityOfGrateGas { get; set; }

    /// <summary>
    /// Теплопроводность колошникового газа, Вт/(м*С)
    /// </summary>
    public double ThermalConductivityOfGrateGas { get; set; }

    /// <summary>
    /// Кинематическая вязкость колошникового газа, м2/с
    /// </summary>
    public double KinematicViscosityOfGrateGas { get; set; }

    /// <summary>
    /// Объемный коэффициент теплообмена, кВт/(м3*°С)
    /// </summary>
    public double VolumetricHeatTransferCoefficient { get; set; }

    /// <summary>
    /// Расход шихты, проходящий через шахту печи, кг/с
    /// </summary>
    public double ChargeFlowRatePassingThroughFurnaceShaft { get; set; }

    /// <summary>
    /// Средняя теплоемкость шихты, кДж/(кг*°С)
    /// </summary>
    public double AverageHeatCapacityOfCharge { get; set; }

    // ПРОМЕЖУТОЧНЫЕ РАСЧЕТНЫЕ ДАННЫЕ
    /// <summary>
    /// Промежуточные расчетные данные (числитель) 
    /// </summary>
    public double IntermediateNumerator { get; set; }

    /// <summary>
    /// Промежуточные расчетные данные (знаменатель) 
    /// </summary>
    public double IntermediateDenominator { get; set; }

    /// <summary>
    /// Промежуточные расчетные данные (отношение) 
    /// </summary>
    public double IntermediateRatio { get; set; }

    /// <summary>
    /// Промежуточные расчетные данные (экспонента) 
    /// </summary>
    public double IntermediateExhibitor { get; set; }

    /// <summary>
    /// Индекс верха печи
    /// </summary>
    public double IndexOfTheFurnaceTop { get; set; }

    // Для расчета теоретической температуры горения углерода кокса.
    /// <summary>
    /// Расход дутья, необходимый для сжигания 1 кг углерода кокса, м3/кг
    /// </summary>
    public double BlastConsumptionRequiredForBurningOneKgOfCarbonCoke { get; set; }

    /// <summary>
    /// Расход дутья, необходимый для конверсии 1 м3 природного газа, м3/м3
    /// </summary>
    public double BlastConsumptionForConversionOfOneMeterCoubOfNaturalGas { get; set; }

    /// <summary>
    /// Выход фурменного газа в расчёте на 1 кг углерода кокса, сгорающего у фурм, м3/кг
    /// </summary>
    public double OutputOfTheTuyereGasBurningAtTheTuyeres { get; set; }

    /// <summary>
    /// Выход фурменного газа из 1 м3 природного газа при его конверсии в фурменном очаге, м3/м3
    /// </summary>
    public double OutputOfTuyereGasOfNaturalGasDuringСonversion { get; set; }

    /// <summary>
    /// Теплоёмкость двухатомных газов при температуре горячего дутья, кДж/(м3 * К)
    /// </summary>
    public double HeatCapacityOfDiatomicGasesAtHotBlastTemperature { get; set; }

    /// <summary>
    /// Теплоёмкость паров воды при температуре горячего дутья, кДж/(м3 * К)
    /// </summary>
    public double HeatCapacityOfWaterVaporAtHotBlastTemperature { get; set; }

    /// <summary>
    /// Теплосодержание горячего дутья за вычетом теплоты разложения влаги дутья, кДж/м3
    /// </summary>
    public double HeatContentOfHotBlast { get; set; }

    /// <summary>
    /// Теплосодержание углерода кокса пришедшего к фурмам, кДж/кг
    /// </summary>
    public double HeatContentOfCarbonOfCokeToTuyeres { get; set; }

    /// <summary>
    /// Расход природного газа в расчете на 1 кг кокса, приходящего к фурмам, м3/кг
    /// </summary>
    public double NaturalGasConsumptionPerOneKgOfCoke { get; set; }

    /// <summary>
    /// Теплосодержание горновых газов, кДж/м3
    /// </summary>
    public double HeatContentOfFurnaceGases { get; set; }

    /// <summary>
    /// Теоретическая температура горения углерода кокса, °С
    /// </summary>
    public double TheoreticalBurningTemperatureOfCarbonCoke { get; set; }

    /// <summary>
    /// Дата проведения расчёта
    /// </summary>
    public DateTime ResultDate { get; set; }

    public Result()
    {
        ResultDate = DateTime.Now;
    }
}