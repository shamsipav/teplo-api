using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class DailyController : ControllerBase
    {
        private readonly IFurnaceWorkParamsService _furnaceWorkParamsService;
        private IValidator<FurnaceBaseParam> _validator;
        public DailyController(IFurnaceWorkParamsService furnaceWorkParamsService, IValidator<FurnaceBaseParam> validator)
        {
            _furnaceWorkParamsService = furnaceWorkParamsService;
            _validator = validator;
        }

        /// <summary>
        /// Получение данных посуточной информации за всё время
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = User.GetUserId();

            List<FurnaceBaseParam> dailyInfoList = await _furnaceWorkParamsService.GetAll(uid, true);

            return Ok(new Response { IsSuccess = true, Result = dailyInfoList });
        }
        
        /// <summary>
        /// Получение посуточной информации по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            FurnaceBaseParam dailyInfo = await _furnaceWorkParamsService.GetSingleAsync(Guid.Parse(id));

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
            ValidationResult validationResult = await _validator.ValidateAsync(dailyInfo);
            
            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            Guid uid = User.GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            if (dailyInfo == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для добавления печи в справочник" });

            await _furnaceWorkParamsService.CreateOrUpdateAsync(dailyInfo, uid);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Суточная информация успешно обновлена", Result = dailyInfo });
        }
        
        /// <summary>
        /// Удаление посуточной информации из справочника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id == null)
                return NotFound(new Response { ErrorMessage = $"Не удалось найти посуточную информацию с идентификатором id = '{id}'" });
            
            FurnaceBaseParam deletedFurnaceBaseParam = await _furnaceWorkParamsService.RemoveAsync(Guid.Parse(id));

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Посуточная информация за {deletedFurnaceBaseParam.Day.ToString("dd.MM.yyyy")} успешно удалена", Result = deletedFurnaceBaseParam });
        }
    }
}
