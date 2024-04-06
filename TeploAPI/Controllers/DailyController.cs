using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DailyController : TeploController
    {
        private IFurnaceService _furnaceService;
        private TeploDBContext _context;
        private IValidator<FurnaceBaseParam> _validator;
        public DailyController(TeploDBContext context, IValidator<FurnaceBaseParam> validator, IFurnaceService furnaceService)
        {
            _context = context;
            _validator = validator;
            _furnaceService = furnaceService;
        }

        /// <summary>
        /// Получение данных по всем печам за всё время
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = GetUserId();

            var furnaces = new List<FurnaceDailyInfo>();
            try
            {
                furnaces = await _context.DailyInfo.AsNoTracking().Where(m => m.UserId.Equals(uid)).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace GetAsync: Ошибка получения данных: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить данные справочника посуточной информации доменных печей" });
            }

            return Ok(new Response { IsSuccess = true, Result = furnaces });
        }
        
        /// <summary>
        /// Получение данных о работе конкретной печи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            var dailyInfo = new FurnaceDailyInfo();
            try
            {
                dailyInfo = await _context.DailyInfo.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace GetByIdAsync: Ошибка получения данных печи с идентификатором id = '{id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить данные о печи с идентификатором id = '{id}'" });
            }

            if (dailyInfo == null)
            {
                return NotFound(new Response { ErrorMessage = $"Не удалось найти данные о печи с идентификатором id = '{id}'" });
            }

            return Ok(new Response { IsSuccess = true, Result = dailyInfo });
        }

        /// <summary>
        /// Добавление посуточной информации о печи в справочник
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(FurnaceDailyInfo dailyInfo)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dailyInfo);
            
            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            if (dailyInfo == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для добавления печи в справочник" });
            
            // Если пришла дата, которая уже была для конкретной печи - обновляем суточную информацию
            var existDaily = await _context.DailyInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FurnaceId == dailyInfo.FurnaceId && f.Day == dailyInfo.Day);

            if (existDaily != null)
            {
                // var updatedDailyInfoId = await _furnaceService.UpdateFurnaceAsync(existDaily, true);
                // if (updatedDailyInfoId == 0)
                //     return StatusCode(500, new Response { ErrorMessage = "Не удалось обновить информацию о работе ДП за текущие сутки" });
            }
            else
            {
                try
                {
                    dailyInfo.Id = Guid.NewGuid();
                    dailyInfo.UserId = uid;
                    await _context.DailyInfo.AddAsync(dailyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/furnace CreateAsync: Ошибка добавления печи: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = "Не удалось добавить печь в справочник" });
                }
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Суточная информация успешно обновлена", Result = dailyInfo });
        }
    }
}
