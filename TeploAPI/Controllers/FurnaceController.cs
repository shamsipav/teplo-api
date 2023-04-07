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
    public class FurnaceController : Controller
    {
        private TeploDBContext _context;
        public FurnaceController(TeploDBContext context)
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

            var furnaces = new List<Furnace>();
            try
            {
                furnaces = await _context.Furnaces.AsNoTracking().Where(f => f.UserId == uid).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/furnace GetAsync: Ошибка получения сохраненных вариантов исходных данных: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить сохраненные варианты исходных данных" });
            }

            return Ok(new Response { IsSuccess = true, Result = furnaces });
        }

        /// <summary>
        /// Получение дефолтного варианта для расчета
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            var furnace = new Furnace();

            try
            {
                furnace = Furnace.GetDefaultData();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/furnace GetDefault: Ошибка получения варианта исходных данных по умолчанию: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить вариант исходных данных по умолчанию" });
            }

            return Ok(new Response { IsSuccess = true, Result = furnace });
        }

        // TODO: Привязать к пользователю.
        /// <summary>
        /// Удаление варианта исходных данных
        /// </summary>
        /// <param name="furnaceId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{furnaceId}")]
        public async Task<IActionResult> DeleteAsync(int? furnaceId)
        {
            if (furnaceId != null)
            {
                var furnace = new Furnace();

                try
                {
                    furnace = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id == furnaceId);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/furnace DeleteAsync: Ошибка получения варианта исходных данных для удаления: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить вариант исходных данных для удаления" });
                }

                if (furnace != null)
                {
                    try
                    {
                        _context.Furnaces.Remove(furnace);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/furnace DeleteAsync: Ошибка удаления варианта исходных данных: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить вариант исходных данных с идентификатором id = '{furnaceId}'" });
                    }

                    return Ok(new Response { IsSuccess = true, Result = furnace });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{furnaceId}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{furnaceId}'" });
        }
    }
}
