using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : TeploController
    {
        private IReferenceCoefficientsService _referenceService;
        private TeploDBContext _context;
        public ReferenceController(TeploDBContext context, IReferenceCoefficientsService referenceService)
        {
            _context = context;
            _referenceService = referenceService;
        }

        // TODO: try, catch, Serilog, Fluent (?)
        /// <summary>
        /// Получение значений для справочника корректировочных коэффициентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var reference = await _referenceService.GetCoefficientsReferenceByUserIdAsync(uid);

            if (reference == null)
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить коэффициенты для справочника" });

            return Ok(new Response { IsSuccess = true, Result = reference });
        }

        // TODO: Проверить, действительно ли нужно использовать ReferenceDTO,
        // лучше по отдельности (для кокса и для пр.)?
        [HttpPost]
        public async Task<IActionResult> PostAsync(Reference reference)
        {
            if (reference.CokeCunsumptionReference != null && reference.FurnaceCapacityReference != null)
            {
                Guid uid = GetUserId();
                if (uid.Equals(Guid.Empty))
                    return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

                var existReference = await _referenceService.GetCoefficientsReferenceByUserIdAsync(uid);

                if (reference == null)
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить текущие коэффициенты для обновления справочника" });

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

                try
                {
                    _context.CokeCunsumptionReferences.Update(existReference.CokeCunsumptionReference);
                    _context.FurnanceCapacityReferences.Update(existReference.FurnaceCapacityReference);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    Log.Error($"HTTP PUT api/reference PutAsync: Ошибка обновления справочника корректировочных коэффициентов: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось обновить справочник корректировочных коэффициентов" });
                }
            }
            else
            {
                return BadRequest(new Response { ErrorMessage = "Необходимо указать значения для обновления справочника корректировочных коэффициентов" });
            }

            return Ok(new Response { IsSuccess = true, Result = reference });
        }
    }
}
