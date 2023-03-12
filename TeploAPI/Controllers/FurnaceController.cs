using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeploAPI.Data;

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
            return Ok(furnances);
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
                var furnace = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id == furnaceId);
                _context.Furnaces.Remove(furnace);
                await _context.SaveChangesAsync();

                return Ok(furnace);
            }

            return NotFound("Не удалось найти информацию об варианте расчета");
        }
    }
}
