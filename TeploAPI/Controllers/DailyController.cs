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
        /// Получение данных посуточной информации за всё время
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = GetUserId();

            var dailyInfo = new List<FurnaceBaseParam>();
            try
            {
                // Имеем в виду, что посуточная информация и варианты исходных данных - одна сущность, отличается только наличием/отсутствием значения в Day
                // По-умолчанию в DateTime устанавливается DateTime.MinValue
                dailyInfo = await _context.FurnacesWorkParams
                                         .Include(d => d.MaterialsWorkParamsList)
                                         .AsNoTracking()
                                         .Where(p => p.UserId.Equals(uid) && p.Day != DateTime.MinValue).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/daily GetAsync: Ошибка получения данных: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить данные справочника посуточной информации доменных печей" });
            }

            return Ok(new Response { IsSuccess = true, Result = dailyInfo });
        }
        
        /// <summary>
        /// Получение данных о работе конкретной печи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            var dailyInfo = new FurnaceBaseParam();
            try
            {
                // Имеем в виду, что посуточная информация и варианты исходных данных - одна сущность, отличается только наличием/отсутствием значения в Day
                // По-умолчанию в DateTime устанавливается DateTime.MinValue
                dailyInfo = await _context.FurnacesWorkParams
                                          .Include(d => d.MaterialsWorkParamsList)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(p => p.Id.Equals(Guid.Parse(id)) && p.Day != DateTime.MinValue);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/daily GetByIdAsync: Ошибка получения посуточной информации с идентификатором id = '{id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить посуточную информацию с идентификатором id = '{id}'" });
            }

            if (dailyInfo == null)
            {
                return NotFound(new Response { ErrorMessage = $"Не удалось найти посуточную информацию с идентификатором id = '{id}'" });
            }

            return Ok(new Response { IsSuccess = true, Result = dailyInfo });
        }

        /// <summary>
        /// Добавление посуточной информации о печи в справочник
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(FurnaceBaseParam dailyInfo)
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
            var existDaily = await _context.FurnacesWorkParams
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FurnaceId.Equals(dailyInfo.FurnaceId) && f.Day == dailyInfo.Day);

            if (existDaily != null)
            {
                dailyInfo.Id = existDaily.Id;
                Guid updatedDailyInfoId = await _furnaceService.UpdateFurnaceAsync(dailyInfo);
                if (updatedDailyInfoId.Equals(Guid.Empty))
                    return StatusCode(500, new Response { ErrorMessage = "Не удалось обновить информацию о работе ДП за текущие сутки" });
            }
            else
            {
                try
                {
                    dailyInfo.Id = Guid.NewGuid();
                    dailyInfo.UserId = uid;
                    await _context.FurnacesWorkParams.AddAsync(dailyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP POST api/daily CreateAsync: Ошибка добавления посуточной информации: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = "Не удалось добавить посуточную информацию в справочник" });
                }
            }

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
            if (id != null)
            {
                var dailyParams = new FurnaceBaseParam();

                try
                {
                    dailyParams = await _context.FurnacesWorkParams.FirstOrDefaultAsync(d => d.Id.Equals(Guid.Parse(id)) && d.Day != DateTime.MinValue);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/daily DeleteAsync: Ошибка получения посуточной информации для удаления: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить посуточную информацию для удаления: {ex}" });
                }

                if (dailyParams != null)
                {
                    try
                    {
                        _context.FurnacesWorkParams.Remove(dailyParams);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/daily DeleteAsync: Ошибка удаления посуточной информации: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить посуточную информацию с идентификатором id = '{id}'" });
                    }

                    return Ok(new Response { IsSuccess = true, SuccessMessage = $"Посуточная информация за {dailyParams.Day.ToString("dd.MM.yyyy")} успешно удалена", Result = dailyParams });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти найти посуточную информацию с идентификатором id = '{id}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти посуточную информацию с идентификатором id = '{id}'" });
        }
    }
}
