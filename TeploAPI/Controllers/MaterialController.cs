using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        /// <summary>
        /// Получение всех значений справочника шихтовых материалов
        /// </summary>
        [HttpGet]
        public IActionResult GetAsync()
        {
            List<Material> materials = _materialService.GetAll();

            return Ok(new Response { IsSuccess = true, Result = materials });
        }

        /// <summary>
        /// Добавление материала в справочник шихтовых материалов
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Material material)
        {
            Material createdMaterial = await _materialService.CreateMaterialAsync(material);

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Материал '{createdMaterial.Name}' успешно создан", Result = createdMaterial });
        }

        /// <summary>
        /// Обновление материала в справочнике
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(Material material)
        {
            Material updatedMaterial = await _materialService.UpdateMaterialAsync(material);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Изменения успешно применены", Result = updatedMaterial });
        }

        /// <summary>
        /// Получение определенного материала
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Material material = await _materialService.GetSingleMaterialAsync(id);

            return Ok(new Response { IsSuccess = true, Result = material });
        }

        /// <summary>
        /// Удаление материала из справочника
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Material material = await _materialService.RemoveMaterialAsync(id);

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Материал '{material.Name}' успешно удален", Result = material });
        }
    }
}