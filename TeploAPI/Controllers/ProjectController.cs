using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectPeriodService _projectPeriodService;

        public ProjectController(IProjectPeriodService projectPeriodService)
        {
            _projectPeriodService = projectPeriodService;
        }

        /// <summary>
        /// Получение результатов расчета теплового режима в проектном периоде
        /// </summary>
        /// <param name="projectPeriodFurnaceData">Исходные данные для проектного периода</param>
        /// <param name="inputDataId">Идентификатор варианта исходных данных базового периода</param>
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceProjectParam projectPeriodFurnaceData, Guid inputDataId)
        {
            UnionResultViewModel result = await _projectPeriodService.ProcessProjectPeriod(projectPeriodFurnaceData, inputDataId);

            return Ok(new Response { IsSuccess = true, Result = result });
        }
    }
}