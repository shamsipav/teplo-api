using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class DailyController : ControllerBase
    {
        private readonly IFurnaceWorkParamsService _furnaceWorkParamsService;

        public DailyController(IFurnaceWorkParamsService furnaceWorkParamsService)
        {
            _furnaceWorkParamsService = furnaceWorkParamsService;
        }

        /// <summary>
        /// Получение данных посуточной информации за всё время
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<FurnaceBaseParam> dailyInfoList = await _furnaceWorkParamsService.GetAll(true);

            return Ok(new Response { IsSuccess = true, Result = dailyInfoList });
        }

        /// <summary>
        /// Получение посуточной информации по идентификатору
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            FurnaceBaseParam dailyInfo = await _furnaceWorkParamsService.GetSingleAsync(id);

            if (dailyInfo == null)
                return NotFound(new Response { ErrorMessage = $"Не удалось найти посуточную информацию с идентификатором id = '{id}'" });

            return Ok(new Response { IsSuccess = true, Result = dailyInfo });
        }

        /// <summary>
        /// Добавление посуточной информации о печи в справочник
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(FurnaceBaseParam dailyInfo)
        {
            await _furnaceWorkParamsService.CreateOrUpdateAsync(dailyInfo);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Суточная информация успешно обновлена", Result = dailyInfo });
        }

        /// <summary>
        /// Удаление посуточной информации из справочника
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            FurnaceBaseParam deletedFurnaceBaseParam = await _furnaceWorkParamsService.RemoveAsync(id);

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Посуточная информация за {deletedFurnaceBaseParam.Day.ToString("dd.MM.yyyy")} успешно удалена", Result = deletedFurnaceBaseParam });
        }
    }
}