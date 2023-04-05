using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SweetAPI.Models;
using SweetAPI.Utils;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Services;
using TeploAPI.ViewModels;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private IFurnaceService _furnaceService;
        private IValidator<Furnace> _validator;
        private TeploDBContext _context;
        public BaseController(TeploDBContext context, IValidator<Furnace> validator, IFurnaceService furnaceService)
        {
            _context = context;
            _validator = validator;
            _furnaceService = furnaceService;
        }

        // TODO: Добавить названия для сохраненных вариантов исходных данных.
        // TODO: Добавить класс Response для унификации результатов методов.
        /// <summary>
        /// Получение результатов расчета теплового режима в базовом периоде
        /// </summary>
        /// <param name="save">Сохранение варианта исзодных данных</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Furnace furnace, bool save)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnace);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors[0].ErrorMessage);

            // TODO: Вынести логику сохранения вариантов в отдельный сервис.
            if (furnace.Id > 0)
            {
                var updatedFurnaceId = await _furnaceService.UpdateFurnaceAsync(furnace);
                if (updatedFurnaceId == 0)
                    return Problem($"Не удалось обновить сохраненный вариант исходных данных с идентификатором id = '{furnace.Id}'");
            }

            if (save && furnace.Id == 0)
            {
                // TODO: Вынести в отдельный сервис код ниже отсюда (а также отрефакторить весь код с проверками на null):
                // TODO: А ЕЩЕ ЛУЧШЕ БУДЕТ В ТОКЕН ВШИТЬ ИДЕНТИФИКАТОР ПОЛЬЗОВАТЕЛЯ (а может и нет).
                var headers = Request.Headers;
                headers.TryGetValue("Authorization", out var authHeader);
                string token = authHeader.ToString().Split(' ').Skip(1).FirstOrDefault();

                string email = Validate.ValidateToken(token);

                var existUser = new User();

                if (email != null)
                {
                    existUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
                    if (existUser is null)
                        return NotFound("Пользователь с таким Email не найден");
                }
                else
                    return BadRequest("Некорректный токен");
                // до сюда.

                var savedFurnaceId = await _furnaceService.SaveFurnaceAsync(furnace, existUser.Id);
                if (savedFurnaceId == 0)
                    return Problem($"Не удалось сохранить новый вариант исходных данных");
            }

            // TODO: Возможно, стоит вынести инициализацию сервиса.
            CalculateService calculate = new CalculateService();

            var calculateResult = new Result();

            try
            {
                calculateResult = calculate.СalculateThermalRegime(furnace);
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP POST api/base Ошибка выполнения расчета: {ex}");
                return Problem($"Не удалось выполнить расчет в базовом периоде: {ex}");
            }

            var result = new ResultViewModel { Input = furnace, Result = calculateResult };

            return Ok(result);
        }
    }
}
