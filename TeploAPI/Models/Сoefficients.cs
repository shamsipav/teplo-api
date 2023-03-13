﻿namespace TeploAPI.Models
{
    // TODO: Rename.
    public class Сoefficients
    {
        public int Id { get; set; }
        /// <summary>
        /// Увеличение массовой доли железа в рудной сыпи на 1 %
        /// </summary>
        public double IronMassFractionIncreaseInOreRash { get; set; }

        /// <summary>
        /// Снижение доли сырой руды в шихте на 1 %
        /// </summary>
        public double ShareCrudeOreReductionCharge { get; set; }

        /// <summary>
        /// Повышение температуры дутья на 10 градусов в пределах 800 - 900 ºC
        /// </summary>
        public double TemperatureIncreaseInRangeOf800to900 { get; set; }

        /// <summary>
        /// Повышение температуры дутья на 10 градусов в пределах 901 - 1000 ºC
        /// </summary>
        public double TemperatureIncreaseInRangeOf901to1000 { get; set; }

        /// <summary>
        /// Повышение температуры дутья на 10 градусов в пределах 1001 - 1100 ºC
        /// </summary>
        public double TemperatureIncreaseInRangeOf1001to1100 { get; set; }

        /// <summary>
        /// Повышение температуры дутья на 10 градусов в пределах 1101 - 1200 ºC
        /// </summary>
        public double TemperatureIncreaseInRangeOf1101to1200 { get; set; }

        /// <summary>
        /// Повышение давления газа под колошником на 0,1 кг/см2 (0,01 Мпа)
        /// </summary>
        public double IncreaseGasPressureUnderGrate { get; set; }

        /// <summary>
        /// Снижение массовой доли кремния в чугуне на 0,1 %
        /// </summary>
        public double ReductionMassFractionOfSiliciumInChugun { get; set; }

        /// <summary>
        /// Снижение массовой доли серы в чугуне на 0,1 %
        /// </summary>
        public double ReductionMassFractionOfSeraInChugun { get; set; }

        /// <summary>
        /// Увеличение массовой доли фосфора в чугуне на 0,1 %
        /// </summary>
        public double IncreaseMassFractionOfPhosphorusInChugun { get; set; }

        /// <summary>
        /// Увеличение массовой доли марганца в чугуне на 0,1 %
        /// </summary>
        public double IncreaseMassFractionOfManganeseInChugun { get; set; }

        /// <summary>
        /// Увеличение массовой доли титана в чугуне на 0,1 % (???)
        /// </summary>
        public double IncreaseMassFractionOfTitanInChugun { get; set; }

        /// <summary>
        /// Увеличение влажности дутья на 1 г/м3 (без увеличения температуры дутья)
        /// </summary>
        public double IncreaseBlastHumidity { get; set; }

        /// <summary>
        /// Увеличение расхода природного газа на 1 м3/т
        /// </summary>
        public double IncreaseNaturalGasCunsimption { get; set; }

        /// <summary>
        /// Вывод из шихты 10 кг/т известняка
        /// </summary>
        public double OutputFromLimestoneCharge { get; set; }

        /// <summary>
        /// Увеличение объемной доли кислорода в дутье на 1 %
        /// </summary>
        public double IncreaseVolumeFractionOxygenInBlast { get; set; }

        /// <summary>
        /// Снижение массовой доли мелочи (фракции 0-5 мм) в рудной сыпи на 1 %
        /// </summary>
        public double ReductionMassFractionTrifles { get; set; }

        /// <summary>
        /// Уменьшение массовой доли золы в коксе в пределах 11 - 12 %
        /// </summary>
        public double ReductionMassFractionAshInCokeInRangeOf11to12Percent { get; set; }

        /// <summary>
        /// Уменьшение массовой доли золы в коксе в пределах 12 - 13 %
        /// </summary>
        public double ReductionMassFractionAshInCokeInRangeOf12to13Percent { get; set; }

        /// <summary>
        /// Уменьшение массовой доли серы в коксе на 0,1 %
        /// </summary>
        public double ReductionMassFractionOfSera { get; set; }
    }
}
