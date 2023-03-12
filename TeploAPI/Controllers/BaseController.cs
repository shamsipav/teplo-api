using Microsoft.AspNetCore.Mvc;
using TeploAPI.Data;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private TeploDBContext _context;
        public BaseController(TeploDBContext context)
        {
            _context = context;
        }

        // TODO: Добавить модель в параметры
        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        /// <param name="save">Сохранение характеристик печи</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync(bool save)
        {
            Furnace furnace = Furnace.GetDefaultData();

            if (save)
            {
                _context.Furnaces.Add(furnace);
                await _context.SaveChangesAsync();
            }

            CalculateService calculate = new CalculateService();

            var calculateResult = calculate.СalculateThermalRegime(furnace);

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(result);
        }
    }
}
