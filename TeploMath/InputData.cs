namespace TeploMath;

public class InputData
{
    /// <summary>
    /// Номер доменной печи
    /// </summary>
    public int NumberOfFurnace { get; set; }

    /// <summary>
    /// Полезный объем печи, м3
    /// </summary>
    public double UsefulVolumeOfFurnace { get; set; }

    /// <summary>
    /// Полезная высота печи, мм
    /// </summary>
    public double UsefulHeightOfFurnace { get; set; }

    /// <summary>
    /// Диаметр колошника, мм
    /// </summary>
    public double DiameterOfColoshnik { get; set; }

    /// <summary>
    /// Диаметр распара, мм
    /// </summary>
    public double DiameterOfRaspar { get; set; }

    /// <summary>
    /// Диаметр горна, мм
    /// </summary>
    public double DiameterOfHorn { get; set; }

    /// <summary>
    /// Высота горна, мм
    /// </summary>
    public double HeightOfHorn { get; set; }

    /// <summary>
    /// Высота фурм, мм
    /// </summary>
    public double HeightOfTuyeres { get; set; }

    /// <summary>
    /// Высота заплечников, мм
    /// </summary>
    public double HeightOfZaplechiks { get; set; }

    /// <summary>
    /// Высота распара, мм
    /// </summary>
    public double HeightOfRaspar { get; set; }

    /// <summary>
    /// Высота шахты, мм
    /// </summary>
    public double HeightOfShaft { get; set; }

    /// <summary>
    /// Высота колошника, мм
    /// </summary>
    public double HeightOfColoshnik { get; set; }

    /// <summary>
    /// Установленный уровень насыпи, мм
    /// </summary>
    public double EstablishedLevelOfEmbankment { get; set; }

    /// <summary>
    /// Число фурм, шт
    /// </summary>
    public double NumberOfTuyeres { get; set; }

    /// <summary>
    /// Суточная производительность печи, т чугуна/сутки
    /// </summary>
    public double DailyСapacityOfFurnace { get; set; }

    /// <summary>
    /// Удельный расход кокса, кг/т чугуна
    /// </summary>
    public double SpecificConsumptionOfCoke { get; set; }

    /// <summary>
    /// Удельный расход ЖРМ, кг/т чугуна
    /// </summary>
    public double SpecificConsumptionOfZRM { get; set; }

    /// <summary>
    /// Доля окатышей в шихте, доли ед.
    /// </summary>
    public double ShareOfPelletsInCharge { get; set; }

    // ПАРАМЕТРЫ ДУТЬЯ
    /// <summary>
    /// Расход дутья, м3/мин
    /// </summary>
    public double BlastConsumption { get; set; }

    /// <summary>
    /// Температура дутья, С
    /// </summary>
    public double BlastTemperature { get; set; }

    /// <summary>
    /// Давление дутья, ати
    /// </summary>
    public double BlastPressure { get; set; }

    /// <summary>
    /// Влажность дутья, г/м3
    /// </summary>
    public double BlastHumidity { get; set; }

    /// <summary>
    /// Содержание кислорода в дутье, %
    /// </summary>
    public double OxygenContentInBlast { get; set; }

    /// <summary>
    /// Расход природного газа, м3/т чугуна
    /// </summary>
    public double NaturalGasConsumption { get; set; }

    // ПАРАМЕТРЫ КОЛОШНИКОВОГО ГАЗА
    /// <summary>
    /// Температура колошникового газа, С
    /// </summary>
    public double ColoshGasTemperature { get; set; }

    /// <summary>
    /// Давление колошникового газа, ати
    /// </summary>
    public double ColoshGasPressure { get; set; }

    // СОСТАВ КОЛОШНИКОВОГО ГАЗА
    /// <summary>
    /// CO в колошниковом газе, %
    /// </summary>
    public double ColoshGas_CO { get; set; }

    /// <summary>
    /// CO2 в колошниковом газе, %
    /// </summary>
    public double ColoshGas_CO2 { get; set; }

    /// <summary>
    /// H2 в колошниковом газе, %
    /// </summary>
    public double ColoshGas_H2 { get; set; }

    // СОСТАВ ЧУГУНА
    /// <summary>
    /// Содержание Si в чугуне, %
    /// </summary>
    public double Chugun_SI { get; set; }

    /// <summary>
    /// Содержание Mn в чугуне, %
    /// </summary>
    public double Chugun_MN { get; set; }

    /// <summary>
    /// Содержание P в чугуне, %
    /// </summary>
    public double Chugun_P { get; set; }

    /// <summary>
    /// Содержание S в чугуне, %
    /// </summary>
    public double Chugun_S { get; set; }

    /// <summary>
    /// Содержание C в чугуне, %
    /// </summary>
    public double Chugun_C { get; set; }


    // СОСТАВ КОКСА
    /// <summary>
    /// Содержание золы в коксе, %
    /// </summary>
    public double AshContentInCoke { get; set; }

    /// <summary>
    /// Содержание летучих в коксе, %
    /// </summary>
    public double VolatileContentInCoke { get; set; }

    /// <summary>
    /// Содержание серы в коксе, %
    /// </summary>
    public double SulfurContentInCoke { get; set; }

    /// <summary>
    /// Удельный выход шлака (по данным тех. отчёта), кг/т чугуна
    /// </summary>
    public double SpecificSlagYield { get; set; }

    /// <summary>
    /// Теплоёмкость агломерата, кДж/(кг * С)
    /// </summary>
    public double HeatCapacityOfAgglomerate { get; set; }

    /// <summary>
    /// Теплоёмкость окатышей, кДж/(кг * С)
    /// </summary>
    public double HeatCapacityOfPellets { get; set; }

    /// <summary>
    /// Теплоёмкость кокса, кДж/(кг * С)
    /// </summary>
    public double HeatCapacityOfCoke { get; set; }

    /// <summary>
    /// Принятое значение температуры "резервной зоны", С
    /// </summary>
    public double AcceptedTemperatureOfBackupZone { get; set; }

    /// <summary>
    /// Доля тепловых потерь через нижнюю часть печи, доли ед.
    /// </summary>
    public double ProportionOfHeatLossesOfLowerPart { get; set; }

    /// <summary>
    /// Средний размер куска шихты, м
    /// </summary>
    public double AverageSizeOfPieceCharge { get; set; }

    // Для расчета теоретической температуры горения углерода кокса.
    /// <summary>
    /// Теплота горения природного газа на фурмах, кДж/м3
    /// </summary>
    public double HeatOfBurningOfNaturalGasOnFarms { get; set; }

    /// <summary>
    /// Теплота неполного горения углерода кокса, кДж/кг
    /// </summary>
    public double HeatOfIncompleteBurningCarbonOfCoke { get; set; }

    /// <summary>
    /// Температура кокса, пришедшего к фурмам, °C
    /// </summary>
    public double TemperatureOfCokeThatCameToTuyeres { get; set; }
}