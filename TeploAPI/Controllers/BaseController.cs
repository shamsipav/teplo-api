using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class BaseController : ControllerBase
    {
        private IBasePeriodService _basePeriodService;

        public BaseController(IBasePeriodService basePeriodService)
        {
            _basePeriodService = basePeriodService;
        }

        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceBaseParam furnaceBase, bool save)
        {
            ResultViewModel result = await _basePeriodService.ProcessBasePeriodAsync(furnaceBase, save);

            return Ok(new Response { IsSuccess = true, Result = result });
        }

        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(Guid basePeriodId, Guid comparativePeriodId)
        {
            UnionResultViewModel result = await _basePeriodService.ProcessComparativePeriodAsync(basePeriodId, comparativePeriodId);

            return Ok(new Response { IsSuccess = true, Result = result });
        }
    }
}