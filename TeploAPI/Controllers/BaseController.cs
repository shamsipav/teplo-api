using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class BaseController : ControllerBase
    {
        private IBasePeriodService _basePeriodService;
        private IValidator<FurnaceBaseParam> _validator;
        public BaseController(IValidator<FurnaceBaseParam> validator, IBasePeriodService basePeriodService)
        {
            _validator = validator;
            _basePeriodService = basePeriodService;
        }

        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(FurnaceBaseParam furnaceBase, bool save)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnaceBase);

            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            ResultViewModel result = await _basePeriodService.ProcessBasePeriod(furnaceBase, save);

            return Ok(new Response { IsSuccess = true, Result = result });
        }

        /// <summary>
        /// Сравнение двух отчетных периодов
        /// </summary>
        /// <param name="basePeriodId">Идентификатор базового периода</param>
        /// <param name="comparativePeriodId">Идентификатор сравнительного периода</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ComparisonAsync(string basePeriodId, string comparativePeriodId)
        {
            if (string.IsNullOrEmpty(basePeriodId))
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных / посуточную информацию для базового периода" });

            if (string.IsNullOrEmpty(comparativePeriodId))
                return BadRequest(new Response { ErrorMessage = "Необходимо указать вариант исходных данных / посуточную информацию для сравнительного периода" });

            if (basePeriodId == comparativePeriodId)
                return BadRequest(new Response { ErrorMessage = "Необходимо указать разные варианты данных или посуточной информации" });

            UnionResultViewModel result = await _basePeriodService.ProcessComparativePeriod(Guid.Parse(basePeriodId), Guid.Parse(comparativePeriodId));

            return Ok(new Response { IsSuccess = true, Result = result });
        }
    }
}
