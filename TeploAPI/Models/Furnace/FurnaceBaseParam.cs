﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TeploAPI.Models.Furnace
{
    /// <summary>
    /// Класс, описывающий характеристики доменной печи, используемые для расчетов
    /// Нужен для хранения вариантов исходных данных, а также посуточной информации о работе доменной печи
    /// </summary>
    public class FurnaceBaseParam : FurnaceBase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        
        public Guid FurnaceId { get; set; }
        
        /// <summary>
        /// Сутки, за которые печь характеризуется всеми параметрами
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// Название исходных данных для доменной печи
        /// </summary>
        public string? Name { get; set; }

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

        /// <summary>
        /// Дата сохранения варианта исходных данных в БД
        /// </summary>
        public DateTime SaveDate { get; set; }
        
        /// <summary>
        /// Список выбранных шихтовых материалов
        /// </summary>
        public List<MaterialsWorkParams> MaterialsWorkParamsList { get; set; }

        // ПОЛУЧЕНИЕ ИСХОДНЫХ ЗНАЧЕНИЙ
        public static FurnaceBaseParam GetDefaultData()
        {
            return new FurnaceBaseParam
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
        }
    }
}
