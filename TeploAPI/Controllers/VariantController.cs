using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : Controller
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
            int uid = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == "uid").Value);
            if (uid == 0)
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var variants = new List<FurnaceBase>();
            try
            {
                variants = await _context.FurnaceBases.AsNoTracking().Where(f => f.UserId == uid).ToListAsync();
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
            var variant = new FurnaceBase();

            try
            {
                variant = FurnaceBase.GetDefaultData();
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
        [HttpDelete("{furnaceId}")]
        public async Task<IActionResult> DeleteAsync(int? variantId)
        {
            if (variantId != null)
            {
                var variant = new FurnaceBase();

                try
                {
                    variant = await _context.FurnaceBases.FirstOrDefaultAsync(d => d.Id == variantId);
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
                        _context.FurnaceBases.Remove(variant);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/variant DeleteAsync: Ошибка удаления варианта исходных данных: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить вариант исходных данных с идентификатором id = '{variantId}'" });
                    }

                    return Ok(new Response { IsSuccess = true, Result = variant });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
        }
    }
}
