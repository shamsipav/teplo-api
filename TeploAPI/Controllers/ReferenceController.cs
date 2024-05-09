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
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceCoefficientsService _referenceService;

        public ReferenceController(IReferenceCoefficientsService referenceService)
        {
            _referenceService = referenceService;
        }

        /// <summary>
        /// Получение значений для справочника корректировочных коэффициентов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Reference reference = await _referenceService.GetCoefficientsReference();

            return Ok(new Response { IsSuccess = true, Result = reference });
        }

        /// <summary>
        /// Изменение значений справочника корректировочных коэффициентов
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> PostAsync(Reference reference)
        {
            Reference updatedReference = await _referenceService.UpdateCoefficientsReference(reference);

            return Ok(new Response { IsSuccess = true, Result = updatedReference });
        }
    }
}