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
        /// Получение всех сохранненых характеристик печей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var furnances = await _context.Furnaces.ToListAsync();
            return Ok(furnances);
        }
    }
}
