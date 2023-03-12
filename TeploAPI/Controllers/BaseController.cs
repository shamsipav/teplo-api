using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            Furnace furnace = Furnace.GetDefaultData();

            CalculateService calculate = new CalculateService();

            var calculateResult = calculate.СalculateThermalRegime(furnace);

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(result);
        }
    }
}
