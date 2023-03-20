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
            var furnances = await _context.Furnaces.AsNoTracking().ToListAsync();
            if (furnances.Any())
            {
                return Ok(furnances);
            }

            return NotFound("Не найдены сохраненные варианты расчета");
        }

        // TODO: Try, catch на обращения к БД
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
                var furnace = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id == furnaceId);
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
