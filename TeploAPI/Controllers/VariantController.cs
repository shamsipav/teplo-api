using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Filters;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class VariantController : ControllerBase
    {
        private TeploDBContext _context;
        public VariantController(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех сохранненых вариантов исходных данных
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Guid uid = User.GetUserId();

            List<FurnaceBaseParam> variants = await _context.FurnacesWorkParams
                                                            .AsNoTracking()
                                                            .Include(i => i.MaterialsWorkParamsList)
                                                            .Where(v => v.UserId.Equals(uid) && v.Day == DateTime.MinValue)
                                                            .OrderBy(v => v.SaveDate)
                                                            .ToListAsync();

            return Ok(new Response { IsSuccess = true, Result = variants });
        }

        /// <summary>
        /// Получение дефолтного варианта для расчета
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            FurnaceBaseParam variant = FurnaceBaseParam.GetDefaultData();

            return Ok(new Response { IsSuccess = true, Result = variant });
        }

        // TODO: Привязать к пользователю.
        /// <summary>
        /// Удаление варианта исходных данных
        /// </summary>
        /// <param name="variantId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{variantId}")]
        public async Task<IActionResult> DeleteAsync(Guid variantId)
        {
            if (variantId == null) 
                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
            
            FurnaceBaseParam variant = await _context.FurnacesWorkParams
                .Include(i => i.MaterialsWorkParamsList)
                .FirstOrDefaultAsync(v => v.Id.Equals(variantId) && v.Day == DateTime.MinValue);

            if (variant == null) 
                return NotFound(new Response { ErrorMessage = $"Не удалось найти информацию об варианте расчета с идентификатором id = '{variantId}'" });
                
            _context.FurnacesWorkParams.Remove(variant);
            await _context.SaveChangesAsync();

            return Ok(new Response { IsSuccess = true, Result = variant, SuccessMessage = $"Вариант исходных данных \"{variant.Name}\" удален" });

        }
    }
}
