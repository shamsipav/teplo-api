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
    public class FurnaceController : ControllerBase
    {
        private readonly IFurnaceService _furnaceService;

        public FurnaceController(IFurnaceService furnaceService)
        {
            _furnaceService = furnaceService;
        }

        /// <summary>
        /// Получение всех значений справочника печей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Furnace> furnaces = await _furnaceService.GetAllAsync();

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
            Furnace createdFurnace = await _furnaceService.CreateFurnaceAsync(furnace);

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Печь №{furnace.NumberOfFurnace} успешно добавлена", Result = createdFurnace });
        }

        /// <summary>
        /// Обновление печи в справочнике
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(Furnace furnace)
        {
            Furnace updatedFurnace = await _furnaceService.UpdateFurnaceAsync(furnace);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Изменения успешно применены", Result = updatedFurnace });
        }

        /// <summary>
        /// Получение определенной печи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Furnace furnace = await _furnaceService.GetSingleFurnaceAsync(id);

            return Ok(new Response { IsSuccess = true, Result = furnace });
        }

        /// <summary>
        /// Удаление печи из справочника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Furnace deletedFurnace = await _furnaceService.RemoveFurnaceAsync(id);

            return Ok(new Response { IsSuccess = true, SuccessMessage = $"Печь №{deletedFurnace.NumberOfFurnace} успешно удалена", Result = deletedFurnace });
        }
    }
}