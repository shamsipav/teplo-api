using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : Controller
    {
        private TeploDBContext _context;
        public ReferenceController(TeploDBContext context)
        {
            _context = context;
        }

        // TODO: try, catch, Serilog, Fluent (?)
        /// <summary>
        /// Получение значений для справочника корректировочных коэффициентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var cokeCoefficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 1);
            var furnanceCapacityCoefficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 2);

            ReferenceDTO reference = new ReferenceDTO{ CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            return Ok(reference);
        }

        // TODO: Проверить, действительно ли нужно использовать ReferenceDTO,
        // лучше по отдельности (для кокса и для пр.)?
        [HttpPost]
        public async Task<IActionResult> PostAsync(ReferenceDTO reference)
        {
            if (reference.CokeCunsumptionCoefficents != null && reference.FurnanceCapacityCoefficents != null)
            {
                var cokeCofficients = new Сoefficients();
                var furnanceCapacityCoefficients = new Сoefficients();

                try
                {
                    cokeCofficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 1);
                    furnanceCapacityCoefficients = await _context.Сoefficients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == 2);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP PUT api/reference PutAsync: Ошибка получения коэффициентов для справочника: {ex}");
                    return Problem($"Не удалось получить коэффициенты для справочника: {ex}");
                }

                if (cokeCofficients != null && furnanceCapacityCoefficients != null)
                {
                    cokeCofficients.IronMassFractionIncreaseInOreRash = reference.CokeCunsumptionCoefficents.IronMassFractionIncreaseInOreRash;
                    cokeCofficients.ShareCrudeOreReductionCharge = reference.CokeCunsumptionCoefficents.ShareCrudeOreReductionCharge;
                    cokeCofficients.TemperatureIncreaseInRangeOf800to900 = reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf800to900;
                    cokeCofficients.TemperatureIncreaseInRangeOf901to1000 = reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf901to1000;
                    cokeCofficients.TemperatureIncreaseInRangeOf1001to1100 = reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1001to1100;
                    cokeCofficients.TemperatureIncreaseInRangeOf1101to1200 = reference.CokeCunsumptionCoefficents.TemperatureIncreaseInRangeOf1101to1200;
                    cokeCofficients.IncreaseGasPressureUnderGrate = reference.CokeCunsumptionCoefficents.IncreaseGasPressureUnderGrate;
                    cokeCofficients.ReductionMassFractionOfSiliciumInChugun = reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSiliciumInChugun;
                    cokeCofficients.ReductionMassFractionOfSeraInChugun = reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSeraInChugun;
                    cokeCofficients.IncreaseMassFractionOfPhosphorusInChugun = reference.CokeCunsumptionCoefficents.IncreaseMassFractionOfPhosphorusInChugun;
                    cokeCofficients.IncreaseMassFractionOfManganeseInChugun = reference.CokeCunsumptionCoefficents.IncreaseMassFractionOfManganeseInChugun;
                    cokeCofficients.IncreaseMassFractionOfTitanInChugun = reference.CokeCunsumptionCoefficents.IncreaseMassFractionOfTitanInChugun;
                    cokeCofficients.IncreaseBlastHumidity = reference.CokeCunsumptionCoefficents.IncreaseBlastHumidity;
                    cokeCofficients.IncreaseNaturalGasCunsimption = reference.CokeCunsumptionCoefficents.IncreaseNaturalGasCunsimption;
                    cokeCofficients.OutputFromLimestoneCharge = reference.CokeCunsumptionCoefficents.OutputFromLimestoneCharge;
                    cokeCofficients.IncreaseVolumeFractionOxygenInBlast = reference.CokeCunsumptionCoefficents.IncreaseVolumeFractionOxygenInBlast;
                    cokeCofficients.ReductionMassFractionTrifles = reference.CokeCunsumptionCoefficents.ReductionMassFractionTrifles;
                    cokeCofficients.ReductionMassFractionAshInCokeInRangeOf11to12Percent = reference.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
                    cokeCofficients.ReductionMassFractionAshInCokeInRangeOf12to13Percent = reference.CokeCunsumptionCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
                    cokeCofficients.ReductionMassFractionOfSera = reference.CokeCunsumptionCoefficents.ReductionMassFractionOfSera;

                    furnanceCapacityCoefficients.IronMassFractionIncreaseInOreRash = reference.FurnanceCapacityCoefficents.IronMassFractionIncreaseInOreRash;
                    furnanceCapacityCoefficients.ShareCrudeOreReductionCharge = reference.FurnanceCapacityCoefficents.ShareCrudeOreReductionCharge;
                    furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf800to900 = reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf800to900;
                    furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf901to1000 = reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf901to1000;
                    furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf1001to1100 = reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1001to1100;
                    furnanceCapacityCoefficients.TemperatureIncreaseInRangeOf1101to1200 = reference.FurnanceCapacityCoefficents.TemperatureIncreaseInRangeOf1101to1200;
                    furnanceCapacityCoefficients.IncreaseGasPressureUnderGrate = reference.FurnanceCapacityCoefficents.IncreaseGasPressureUnderGrate;
                    furnanceCapacityCoefficients.ReductionMassFractionOfSiliciumInChugun = reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSiliciumInChugun;
                    furnanceCapacityCoefficients.ReductionMassFractionOfSeraInChugun = reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSeraInChugun;
                    furnanceCapacityCoefficients.IncreaseMassFractionOfPhosphorusInChugun = reference.FurnanceCapacityCoefficents.IncreaseMassFractionOfPhosphorusInChugun;
                    furnanceCapacityCoefficients.IncreaseMassFractionOfManganeseInChugun = reference.FurnanceCapacityCoefficents.IncreaseMassFractionOfManganeseInChugun;
                    furnanceCapacityCoefficients.IncreaseMassFractionOfTitanInChugun = reference.FurnanceCapacityCoefficents.IncreaseMassFractionOfTitanInChugun;
                    furnanceCapacityCoefficients.IncreaseBlastHumidity = reference.FurnanceCapacityCoefficents.IncreaseBlastHumidity;
                    furnanceCapacityCoefficients.IncreaseNaturalGasCunsimption = reference.FurnanceCapacityCoefficents.IncreaseNaturalGasCunsimption;
                    furnanceCapacityCoefficients.OutputFromLimestoneCharge = reference.FurnanceCapacityCoefficents.OutputFromLimestoneCharge;
                    furnanceCapacityCoefficients.IncreaseVolumeFractionOxygenInBlast = reference.FurnanceCapacityCoefficents.IncreaseVolumeFractionOxygenInBlast;
                    furnanceCapacityCoefficients.ReductionMassFractionTrifles = reference.FurnanceCapacityCoefficents.ReductionMassFractionTrifles;
                    furnanceCapacityCoefficients.ReductionMassFractionAshInCokeInRangeOf11to12Percent = reference.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf11to12Percent;
                    furnanceCapacityCoefficients.ReductionMassFractionAshInCokeInRangeOf12to13Percent = reference.FurnanceCapacityCoefficents.ReductionMassFractionAshInCokeInRangeOf12to13Percent;
                    furnanceCapacityCoefficients.ReductionMassFractionOfSera = reference.FurnanceCapacityCoefficents.ReductionMassFractionOfSera;
                }
                else
                {
                    return NotFound("Не найдены данные в справочнике корректировочных коэффициентов");
                }

                try
                {
                    _context.Сoefficients.Update(cokeCofficients);
                    _context.Сoefficients.Update(furnanceCapacityCoefficients);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    Log.Error($"HTTP PUT api/reference PutAsync: Ошибка обновления справочника корректировочных коэффициентов: {ex}");
                    return Problem($"Не удалось обновить справочник корректировочных коэффициентов: {ex}");
                }
            }
            else
            {
                return BadRequest("Необходимо указать значения для обновления справочника корректировочных коэффициентов");
            }

            return Ok(reference);
        }
    }
}
