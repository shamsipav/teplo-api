using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : TeploController
    {
        private TeploDBContext _context;
        public VariantController(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех сохранненых вариантов исходных данных
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var variants = new List<FurnaceBaseParam>();
            try
            {
                // Имеем в виду, что посуточная информация и варианты исходных данных - одна сущность, отличается только наличием/отсутствием значения в Day
                // По-умолчанию в DateTime устанавливается DateTime.MinValue
                variants = await _context.FurnacesWorkParams
                                         .AsNoTracking()
                                         .Where(v => v.UserId.Equals(uid) && v.Day == DateTime.MinValue)
                                         .OrderBy(v => v.SaveDate)
                                         .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/variant GetAsync: Ошибка получения сохраненных вариантов исходных данных: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить сохраненные варианты исходных данных" });
            }

            return Ok(new Response { IsSuccess = true, Result = variants });
        }

        /// <summary>
        /// Получение дефолтного варианта для расчета
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            var variant = new FurnaceBaseParam();

            try
            {
                variant = FurnaceBaseParam.GetDefaultData();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/variant GetDefault: Ошибка получения варианта исходных данных по умолчанию: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить вариант исходных данных по умолчанию" });
            }

            return Ok(new Response { IsSuccess = true, Result = variant });
        }

        // TODO: Привязать к пользователю.
        /// <summary>
        /// Удаление варианта исходных данных
        /// </summary>
        /// <param name="variantId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{variantId}")]
        public async Task<IActionResult> DeleteAsync(string variantId)
        {
            if (variantId != null)
            {
                var variant = new FurnaceBaseParam();

                try
                {
                    // Имеем в виду, что посуточная информация и варианты исходных данных - одна сущность, отличается только наличием/отсутствием значения в Day
                    // По-умолчанию в DateTime устанавливается DateTime.MinValue
                    variant = await _context.FurnacesWorkParams
                                            .FirstOrDefaultAsync(v => v.Id.Equals(Guid.Parse(variantId)) && v.Day == DateTime.MinValue);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/variant DeleteAsync: Ошибка получения варианта исходных данных для удаления: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить вариант исходных данных для удаления" });
                }

                if (variant != null)
                {
                    try
                    {
                        _context.FurnacesWorkParams.Remove(variant);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/variant DeleteAsync: Ошибка удаления варианта исходных данных: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить вариант исходных данных с идентификатором id = '{variantId}'" });
                    }

                    return Ok(new Response { IsSuccess = true, Result = variant, SuccessMessage = $"Вариант исходных данных \"{variant.Name}\" удален" });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
        }
    }
}
