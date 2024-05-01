using System.Security.Claims;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services
{
    public class ReferenceService : IReferenceCoefficientsService
    {
        private readonly IRepository<CokeCunsumptionReference> _cokeConsumptionReferenceRepository;
        private readonly IRepository<FurnaceCapacityReference> _furnaceCapacityReferenceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReferenceService(IRepository<CokeCunsumptionReference> cokeConsumptionReferenceRepository,
            IRepository<FurnaceCapacityReference> furnaceCapacityReferenceRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _cokeConsumptionReferenceRepository = cokeConsumptionReferenceRepository;
            _furnaceCapacityReferenceRepository = furnaceCapacityReferenceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        /// <summary>
        /// Получение значений справочника корректировочных коэффициентов для текущего пользователя
        /// </summary>
        public Reference GetCoefficientsReference()
        {
            Guid userId = _user.GetUserId();

            CokeCunsumptionReference cokeCoefficients = _cokeConsumptionReferenceRepository.GetSingle(x => x.UserId == userId);
            if (cokeCoefficients == null)
                throw new NoContentException("Не удалось получить значения справочника корректировочных коэффициентов, влияющих на расход кокса");

            FurnaceCapacityReference furnanceCapacityCoefficients = _furnaceCapacityReferenceRepository.GetSingle(x => x.UserId == userId);
            if (furnanceCapacityCoefficients == null)
                throw new NoContentException("Не удалось получить значения справочника корректировочных коэффициентов, влияющих на производительность печи");

            return new Reference { CokeCunsumptionReference = cokeCoefficients, FurnaceCapacityReference = furnanceCapacityCoefficients };
        }

        /// <summary>
        /// Обновление значений справочника корректировочных коэффициентов для текущего пользователя
        /// </summary>
        public async Task<Reference> UpdateCoefficientsReference(Reference reference)
        {
            Reference existReference = GetCoefficientsReference();

            existReference.CokeCunsumptionReference.IronMassFractionIncreaseInOreRash = reference.CokeCunsumptionReference.IronMassFractionIncreaseInOreRash;
            existReference.CokeCunsumptionReference.ShareCrudeOreReductionCharge = reference.CokeCunsumptionReference.ShareCrudeOreReductionCharge;
            existReference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf800to900 = reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf800to900;
            existReference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf901to1000 = reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf901to1000;
            existReference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1001to1100 = reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1001to1100;
            existReference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1101to1200 = reference.CokeCunsumptionReference.TemperatureIncreaseInRangeOf1101to1200;
            existReference.CokeCunsumptionReference.IncreaseGasPressureUnderGrate = reference.CokeCunsumptionReference.IncreaseGasPressureUnderGrate;
            existReference.CokeCunsumptionReference.ReductionMassFractionOfSiliciumInChugun = reference.CokeCunsumptionReference.ReductionMassFractionOfSiliciumInChugun;
            existReference.CokeCunsumptionReference.ReductionMassFractionOfSeraInChugun = reference.CokeCunsumptionReference.ReductionMassFractionOfSeraInChugun;
            existReference.CokeCunsumptionReference.IncreaseMassFractionOfPhosphorusInChugun = reference.CokeCunsumptionReference.IncreaseMassFractionOfPhosphorusInChugun;
            existReference.CokeCunsumptionReference.IncreaseMassFractionOfManganeseInChugun = reference.CokeCunsumptionReference.IncreaseMassFractionOfManganeseInChugun;
            existReference.CokeCunsumptionReference.IncreaseMassFractionOfTitanInChugun = reference.CokeCunsumptionReference.IncreaseMassFractionOfTitanInChugun;
            existReference.CokeCunsumptionReference.IncreaseBlastHumidity = reference.CokeCunsumptionReference.IncreaseBlastHumidity;
            existReference.CokeCunsumptionReference.IncreaseNaturalGasCunsimption = reference.CokeCunsumptionReference.IncreaseNaturalGasCunsimption;
            existReference.CokeCunsumptionReference.OutputFromLimestoneCharge = reference.CokeCunsumptionReference.OutputFromLimestoneCharge;
            existReference.CokeCunsumptionReference.IncreaseVolumeFractionOxygenInBlast = reference.CokeCunsumptionReference.IncreaseVolumeFractionOxygenInBlast;
            existReference.CokeCunsumptionReference.ReductionMassFractionTrifles = reference.CokeCunsumptionReference.ReductionMassFractionTrifles;
            existReference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent = reference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
            existReference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent = reference.CokeCunsumptionReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
            existReference.CokeCunsumptionReference.ReductionMassFractionOfSera = reference.CokeCunsumptionReference.ReductionMassFractionOfSera;

            existReference.FurnaceCapacityReference.IronMassFractionIncreaseInOreRash = reference.FurnaceCapacityReference.IronMassFractionIncreaseInOreRash;
            existReference.FurnaceCapacityReference.ShareCrudeOreReductionCharge = reference.FurnaceCapacityReference.ShareCrudeOreReductionCharge;
            existReference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf800to900 = reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf800to900;
            existReference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf901to1000 = reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf901to1000;
            existReference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1001to1100 = reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1001to1100;
            existReference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1101to1200 = reference.FurnaceCapacityReference.TemperatureIncreaseInRangeOf1101to1200;
            existReference.FurnaceCapacityReference.IncreaseGasPressureUnderGrate = reference.FurnaceCapacityReference.IncreaseGasPressureUnderGrate;
            existReference.FurnaceCapacityReference.ReductionMassFractionOfSiliciumInChugun = reference.FurnaceCapacityReference.ReductionMassFractionOfSiliciumInChugun;
            existReference.FurnaceCapacityReference.ReductionMassFractionOfSeraInChugun = reference.FurnaceCapacityReference.ReductionMassFractionOfSeraInChugun;
            existReference.FurnaceCapacityReference.IncreaseMassFractionOfPhosphorusInChugun = reference.FurnaceCapacityReference.IncreaseMassFractionOfPhosphorusInChugun;
            existReference.FurnaceCapacityReference.IncreaseMassFractionOfManganeseInChugun = reference.FurnaceCapacityReference.IncreaseMassFractionOfManganeseInChugun;
            existReference.FurnaceCapacityReference.IncreaseMassFractionOfTitanInChugun = reference.FurnaceCapacityReference.IncreaseMassFractionOfTitanInChugun;
            existReference.FurnaceCapacityReference.IncreaseBlastHumidity = reference.FurnaceCapacityReference.IncreaseBlastHumidity;
            existReference.FurnaceCapacityReference.IncreaseNaturalGasCunsimption = reference.FurnaceCapacityReference.IncreaseNaturalGasCunsimption;
            existReference.FurnaceCapacityReference.OutputFromLimestoneCharge = reference.FurnaceCapacityReference.OutputFromLimestoneCharge;
            existReference.FurnaceCapacityReference.IncreaseVolumeFractionOxygenInBlast = reference.FurnaceCapacityReference.IncreaseVolumeFractionOxygenInBlast;
            existReference.FurnaceCapacityReference.ReductionMassFractionTrifles = reference.FurnaceCapacityReference.ReductionMassFractionTrifles;
            existReference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent = reference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
            existReference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent = reference.FurnaceCapacityReference.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
            existReference.FurnaceCapacityReference.ReductionMassFractionOfSera = reference.FurnaceCapacityReference.ReductionMassFractionOfSera;
            
            await _cokeConsumptionReferenceRepository.UpdateAsync(existReference.CokeCunsumptionReference);
            await _furnaceCapacityReferenceRepository.UpdateAsync(existReference.FurnaceCapacityReference);

            return existReference;
        }
    }
}