namespace TeploAPI.Models
{
    /// <summary>
    /// Характеристики доменной печи
    /// </summary>
    public class Furnace
    {
        /// <summary>
        /// Первичный ключ для сохранения вариантов исходных данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер доменной печи
        /// </summary>
        public double NumberOfFurnace { get; set; }

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

        // ПОЛУЧЕНИЕ ИСХОДНЫХ ЗНАЧЕНИЙ
        public static Furnace GetDefaultData()
        {
            return new Furnace
            {
                NumberOfFurnace = 10,
                UsefulVolumeOfFurnace = 2014,
                UsefulHeightOfFurnace = 29400,
                DiameterOfColoshnik = 7400,
                DiameterOfRaspar = 10900,
                DiameterOfHorn = 9750,
                HeightOfHorn = 3600,
                HeightOfTuyeres = 3200,
                HeightOfZaplechiks = 3000,
                HeightOfRaspar = 1700,
                HeightOfShaft = 18200,
                HeightOfColoshnik = 2320,
                EstablishedLevelOfEmbankment = 1000,
                NumberOfTuyeres = 25,
                DailyСapacityOfFurnace = 5020,
                SpecificConsumptionOfCoke = 444,
                SpecificConsumptionOfZRM = 1663.1,
                ShareOfPelletsInCharge = 0.3666,
                BlastConsumption = 4583,
                BlastTemperature = 1003,
                BlastPressure = 2.73,
                BlastHumidity = 9,
                OxygenContentInBlast = 27.48,
                NaturalGasConsumption = 127.3,
                ColoshGasTemperature = 211,
                ColoshGasPressure = 1.31,
                ColoshGas_CO = 24.35,
                ColoshGas_CO2 = 17.82,
                ColoshGas_H2 = 8.9,
                Chugun_SI = 0.672,
                Chugun_MN = 0.228,
                Chugun_P = 0.058,
                Chugun_S = 0.018,
                Chugun_C = 4.693,
                AshContentInCoke = 12.78,
                VolatileContentInCoke = 1.31,
                SulfurContentInCoke = 0.48,
                SpecificSlagYield = 321,
                HeatCapacityOfAgglomerate = 0.75,
                HeatCapacityOfPellets = 0.8,
                HeatCapacityOfCoke = 1.09,
                AcceptedTemperatureOfBackupZone = 950,
                ProportionOfHeatLossesOfLowerPart = 0,
                AverageSizeOfPieceCharge = 0.018
            };
        }
    }
}
