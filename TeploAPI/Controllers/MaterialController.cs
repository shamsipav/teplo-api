using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private TeploDBContext _context;
        public MaterialController(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех значений справочника шихтовых материалов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var materials = new List<Material>();
            try
            {
                materials = await _context.Materials.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material GetAsync: Ошибка получения значений справочника шихтовых материалов: {ex}");
                return Problem($"Не удалось получить значения справочника шихтовых материалов: {ex}");
            }

            return Ok(materials);
        }

        /// <summary>
        /// Добавление материала в справочник шихтовых материалов
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Material material)
        {
            // Добавить fluentValidation.

            try
            {
                material.BaseOne = material.CaO / material.SiO2;
                await _context.Materials.AddAsync(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/material CreateAsync: Ошибка добавления материала: {ex}");
                return Problem($"Не удалось добавить материал в справочник: {ex}");
            }

            return Ok(material);
        }

        /// <summary>
        /// Обновление материала в справочнике
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(Material material)
        {
            var existMaterial = new Material();
            try
            {
                existMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.Id == material.Id);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material UpdateByIdAsync: Ошибка получения материала с идентификатором id = '{material.Id}: {ex}");
                return Problem($"Не удалось получить материал с идентификатором id = '{material.Id}': {ex}");
            }

            if (existMaterial == null)
            {
                return NotFound($"Не удалось найти информацию о материале с идентификатором id = '{material.Id}'");
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
                return Problem($"Не удалось обновить материал с идентификатором id = '{material.Id}': {ex}");
            }

            return Ok(existMaterial);
        }

        /// <summary>
        /// Получение определенного материала
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int? materialId)
        {
            var material = new Material();
            try
            {
                material = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == materialId);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP GET api/material GetByIdAsync: Ошибка получения материала с идентификатором id = '{materialId}: {ex}");
                return Problem($"Не удалось получить материал с идентификатором id = '{materialId}': {ex}");
            }

            if (material == null)
            {
                return NotFound($"Не удалось найти информацию о материале с идентификатором id = '{materialId}'");
            }

            return Ok(material);
        }

        /// <summary>
        /// Удаление материала из справочника
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? materialId)
        {
            if (materialId != null)
            {
                var material = new Material();

                try
                {
                    material = await _context.Materials.FirstOrDefaultAsync(d => d.Id == materialId);
                }
                catch (Exception ex)
                {
                    Log.Error($"HTTP DELETE api/material DeleteAsync: Ошибка получения материала для удаления: {ex}");
                    return Problem($"Не удалось получить материал для удаления: {ex}");
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
                        return Problem($"Не удалось удалить материал с идентификатором id = '{materialId}': {ex}");
                    }

                    return Ok(material);
                }

                return NotFound($"Не удалось найти информацию о материале с идентификатором id = '{materialId}'");
            }

            return NotFound($"Не удалось найти информацию о материале с идентификатором id = '{materialId}'");
        }
    }
}
