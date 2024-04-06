using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FurnaceController : TeploController
    {
        // TODO: Добавить валидатор!
        private TeploDBContext _context;
        public FurnaceController(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех значений справочника печей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var furnaces = new List<Furnace>();
            try
            {
                furnaces = await _context.Furnaces.AsNoTracking().Where(m => m.UserId.Equals(uid)).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace GetAsync: Ошибка получения значений справочника печей: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить значения справочника печей" });
            }

            return Ok(new Response { IsSuccess = true, Result = furnaces });
        }

        /// <summary>
        /// Добавление печи в справочник
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Furnace furnace)
        {
            Guid uid = GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            if (furnace == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для добавления печи в справочник" });

            //ValidationResult validationResult = await _validator.ValidateAsync(material);

            //if (!validationResult.IsValid)
            //    return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            try
            {
                furnace.UserId = uid;
                await _context.Furnaces.AddAsync(furnace);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/furnace CreateAsync: Ошибка добавления печи: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось добавить печь в справочник" });
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Печь №{furnace.NumberOfFurnace} успешно добавлена", Result = furnace });
        }

        /// <summary>
        /// Обновление печи в справочнике
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(Furnace furnace)
        {
            if (furnace == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для обновления материала в справочнике" });

            //ValidationResult validationResult = await _validator.ValidateAsync(material);

            //if (!validationResult.IsValid)
            //    return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            var existFurnace = new Furnace();
            try
            {
                existFurnace = await _context.Furnaces.FirstOrDefaultAsync(m => m.Id == furnace.Id);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace UpdateByIdAsync: Ошибка получения печи с идентификатором id = '{furnace.Id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить печь с идентификатором id = '{furnace.Id}'" });
            }

            if (existFurnace == null)
            {
                return NotFound(StatusCode(500, new Response { ErrorMessage = $"Не удалось найти информацию о печи с идентификатором id = '{furnace.Id}'" }));
            }

            try
            {
                existFurnace.NumberOfFurnace = furnace.NumberOfFurnace;
                existFurnace.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
                existFurnace.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
                existFurnace.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
                existFurnace.DiameterOfRaspar = furnace.DiameterOfRaspar;
                existFurnace.DiameterOfHorn = furnace.DiameterOfHorn;
                existFurnace.HeightOfHorn = furnace.HeightOfHorn;
                existFurnace.HeightOfTuyeres = furnace.HeightOfTuyeres;
                existFurnace.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
                existFurnace.HeightOfRaspar = furnace.HeightOfRaspar;
                existFurnace.HeightOfShaft = furnace.HeightOfShaft;
                existFurnace.HeightOfColoshnik = furnace.HeightOfColoshnik;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace UpdateByIdAsync: Ошибка обновления печи с идентификатором id = '{furnace.Id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось обновить печь с идентификатором id = '{furnace.Id}'" });
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Изменения успешно применены", Result = existFurnace });
        }

        /// <summary>
        /// Получение определенной печи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            var furnace = new Furnace();
            try
            {
                furnace = await _context.Furnaces.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/furnace GetByIdAsync: Ошибка получения печи с идентификатором id = '{id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить печь с идентификатором id = '{id}'" });
            }

            if (furnace == null)
            {
                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о печи с идентификатором id = '{id}'" });
            }

            return Ok(new Response { IsSuccess = true, Result = furnace });
        }

        /// <summary>
        /// Удаление печи из справочника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id != null)
            {
                var furnace = new Furnace();

                try
                {
                    furnace = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id.Equals(Guid.Parse(id)));
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/furnace DeleteAsync: Ошибка получения печи для удаления: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить печь для удаления: {ex}" });
                }

                if (furnace != null)
                {
                    try
                    {
                        _context.Furnaces.Remove(furnace);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/furnace DeleteAsync: Ошибка удаления печи: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить печь с идентификатором id = '{id}'" });
                    }

                    return Ok(new Response { IsSuccess = true, SuccessMessage = $"Печь №{furnace.NumberOfFurnace} успешно удалена", Result = furnace });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о печи с идентификатором id = '{id}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о печи с идентификатором id = '{id}'" });
        }
    }
}
