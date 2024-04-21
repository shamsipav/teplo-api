using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Filters;
using TeploAPI.Models;
using TeploAPI.Repositories;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class MaterialController : ControllerBase
    {
        private IValidator<Material> _validator;
        private TeploDBContext _context;
        public MaterialController(TeploDBContext context, IValidator<Material> validator)
        {
            _context = context;
            _validator = validator;
        }

        /// <summary>
        /// Получение всех значений справочника шихтовых материалов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = User.GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            var materials = new List<Material>();
            try
            {
                materials = await _context.Materials.AsNoTracking().Where(m => m.UserId.Equals(uid)).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material GetAsync: Ошибка получения значений справочника шихтовых материалов: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить значения справочника шихтовых материалов" });
            }

            return Ok(new Response { IsSuccess = true, Result = materials });
        }

        /// <summary>
        /// Добавление материала в справочник шихтовых материалов
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Material material)
        {
            Guid uid = User.GetUserId();
            if (uid.Equals(Guid.Empty))
                return StatusCode(401, new Response { ErrorMessage = "Не удалось найти идентификатор пользователя в Claims" });

            if (material == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для добавления материала в справочник" });

            ValidationResult validationResult = await _validator.ValidateAsync(material);

            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            try
            {
                material.UserId = uid;
                material.BaseOne = material.CaO / material.SiO2;

                await _context.Materials.AddAsync(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/material CreateAsync: Ошибка добавления материала: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось добавить материал в справочник" });
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Материал '{material.Name}' успешно создан", Result = material });
        }

        /// <summary>
        /// Обновление материала в справочнике
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(Material material)
        {
            if (material == null)
                return BadRequest(new Response { ErrorMessage = "Отсутсвуют значения для обновления материала в справочнике" });

            ValidationResult validationResult = await _validator.ValidateAsync(material);

            if (!validationResult.IsValid)
                return BadRequest(new Response { ErrorMessage = validationResult.Errors[0].ErrorMessage });

            var existMaterial = new Material();
            try
            {
                existMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.Id == material.Id);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material UpdateByIdAsync: Ошибка получения материала с идентификатором id = '{material.Id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить материал с идентификатором id = '{material.Id}'" });
            }

            if (existMaterial == null)
            {
                return NotFound(StatusCode(500, new Response { ErrorMessage = $"Не удалось найти информацию о материале с идентификатором id = '{material.Id}'" }));
            }

            try
            {
                existMaterial.Name = material.Name;
                existMaterial.Moisture = material.Moisture;
                existMaterial.Fe2O3 = material.Fe2O3;
                existMaterial.Fe = material.Fe;
                existMaterial.FeO = material.FeO;
                existMaterial.CaO = material.CaO;
                existMaterial.SiO2 = material.SiO2;
                existMaterial.MgO = material.MgO;
                existMaterial.Al2O3 = material.Al2O3;
                existMaterial.TiO2 = material.TiO2;
                existMaterial.MnO = material.MnO;
                existMaterial.P = material.P;
                existMaterial.S = material.S;
                existMaterial.Zn = material.Zn;
                existMaterial.Mn = material.Mn;
                existMaterial.Cr = material.Cr;
                existMaterial.FiveZero = material.FiveZero;
                existMaterial.BaseOne = material.CaO / material.SiO2;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material UpdateByIdAsync: Ошибка обновления материала с идентификатором id = '{material.Id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось обновить материал с идентификатором id = '{material.Id}'" });
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Изменения успешно применены", Result = existMaterial });
        }

        /// <summary>
        /// Получение определенного материала
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string? id)
        {
            var material = new Material();
            try
            {
                material = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material GetByIdAsync: Ошибка получения материала с идентификатором id = '{id}: {ex}");
                return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить материал с идентификатором id = '{id}'" });
            }

            if (material == null)
            {
                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о материале с идентификатором id = '{id}'" });
            }

            return Ok(new Response { IsSuccess = true, Result = material });
        }

        /// <summary>
        /// Удаление материала из справочника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id != null)
            {
                var material = new Material();

                try
                {
                    material = await _context.Materials.FirstOrDefaultAsync(d => d.Id.Equals(Guid.Parse(id)));
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/material DeleteAsync: Ошибка получения материала для удаления: {ex}");
                    return StatusCode(500, new Response { ErrorMessage = $"Не удалось получить материал для удаления: {ex}" });
                }

                if (material != null)
                {
                    try
                    {
                        _context.Materials.Remove(material);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"HTTP DELETE api/material DeleteAsync: Ошибка удаления материала: {ex}");
                        return StatusCode(500, new Response { ErrorMessage = $"Не удалось удалить материал с идентификатором id = '{id}'" });
                    }

                    return Ok(new Response { IsSuccess = true, SuccessMessage = $"Материал '{material.Name}' успешно удален", Result = material });
                }

                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о материале с идентификатором id = '{id}'" });
            }

            return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию о материале с идентификатором id = '{id}'" });
        }
    }
}
