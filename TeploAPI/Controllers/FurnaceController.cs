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
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var furnaces = new List<Furnace>();
            try
            {
                furnaces = await _context.Furnaces.AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                Log.Error($"HTTP POST api/furnace GetAsync: Ошибка получения сохраненных вариантов исходных данных: {ex}");
                return Problem($"Не удалось получить сохраненные варианты исходных данных: {ex}");
            }

            if (furnaces.Any())
            {
                return Ok(furnaces);
            }

            return NotFound("Не найдены сохраненные варианты расчета");
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
                return Problem($"Не удалось получить вариант исходных данных по умолчанию: {ex}");
            }

            return Ok(furnace);
        }

        /// <summary>
        /// Удаление варианта исходных данных
        /// </summary>
        /// <param name="furnaceId"></param>
        /// <returns></returns>
        [HttpDelete]
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
                    return Problem($"Не удалось получить вариант исходных данных для удаления: {ex}");
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
                        return Problem($"Не удалось удалить вариант исходных данных: {ex}");
                    }

                    return Ok(furnace);
                }

                return NotFound("Не удалось найти информацию об варианте расчета");
            }

            return NotFound("Не удалось найти информацию об варианте расчета");
        }
    }
}
