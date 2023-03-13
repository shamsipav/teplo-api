using System.ComponentModel.DataAnnotations.Schema;

namespace TeploAPI.Models
{
    public class Reference
    {
        public int Id { get; set; }

        public int? RefId1 { get; set; }

        public int? RefId2 { get; set; }

        [ForeignKey(nameof(RefId1))]
        public Сoefficients? CokeCunsumptionCoefficents { get; set; }

        [ForeignKey(nameof(RefId2))]
        public Сoefficients? FurnanceCapacityCoefficents { get; set; }

        public static Reference GetDefaultCoefficients()
        {
            return new Reference
            {
                Id = 1,
                RefId1 = 1,
                RefId2 = 1,
                CokeCunsumptionCoefficents = new Сoefficients
                {
                    Id = 1,
                    IronMassFractionIncreaseInOreRash = -1.0,
                    ShareCrudeOreReductionCharge = -0.2,
                    TemperatureIncreaseInRangeOf800to900 = -0.33,
                    TemperatureIncreaseInRangeOf901to1000 = -0.3,
                    TemperatureIncreaseInRangeOf1001to1100 = -0.23,
                    TemperatureIncreaseInRangeOf1101to1200 = -0.2,
                    IncreaseGasPressureUnderGrate = -0.2,
                    ReductionMassFractionOfSiliciumInChugun = -1.2,
                    ReductionMassFractionOfSeraInChugun = 1.0,
                    IncreaseMassFractionOfPhosphorusInChugun = 1.2,
                    IncreaseMassFractionOfManganeseInChugun = 0.2,
                    IncreaseMassFractionOfTitanInChugun = 1.3,
                    IncreaseBlastHumidity = 0.15,
                    IncreaseNaturalGasCunsimption = -0.7,
                    OutputFromLimestoneCharge = -0.5,
                    IncreaseVolumeFractionOxygenInBlast = 0.3,
                    ReductionMassFractionTrifles = -0.5,
                    ReductionMassFractionAshInCokeInRangeOf11to12Percent = -1.5,
                    ReductionMassFractionAshInCokeInRangeOf12to13Percent = -2.0,
                    ReductionMassFractionOfSera = -0.3
                },
                FurnanceCapacityCoefficents = new Сoefficients
                {
                    Id = 2,
                    IronMassFractionIncreaseInOreRash = 1.7,
                    ShareCrudeOreReductionCharge = 0.2,
                    TemperatureIncreaseInRangeOf800to900 = 0.2,
                    TemperatureIncreaseInRangeOf901to1000 = 0.15,
                    TemperatureIncreaseInRangeOf1001to1100 = 0.12,
                    TemperatureIncreaseInRangeOf1101to1200 = 0.1,
                    IncreaseGasPressureUnderGrate = 1.0,
                    ReductionMassFractionOfSiliciumInChugun = 1.2,
                    ReductionMassFractionOfSeraInChugun = -1.0,
                    IncreaseMassFractionOfPhosphorusInChugun = -1.2,
                    IncreaseMassFractionOfManganeseInChugun = -0.2,
                    IncreaseMassFractionOfTitanInChugun = -1.3,
                    IncreaseBlastHumidity = -0.07,
                    IncreaseNaturalGasCunsimption = 0,
                    OutputFromLimestoneCharge = 0.5,
                    IncreaseVolumeFractionOxygenInBlast = 2.1,
                    ReductionMassFractionTrifles = 1.0,
                    ReductionMassFractionAshInCokeInRangeOf11to12Percent = 1.5,
                    ReductionMassFractionAshInCokeInRangeOf12to13Percent = 1.8,
                    ReductionMassFractionOfSera = 0.3
                }
            };
        }
    }
}
