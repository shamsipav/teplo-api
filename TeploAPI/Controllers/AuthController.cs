using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweetAPI.Models;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    /// <summary>
    /// Авторизация и аутентификация пользователя
    /// </summary>
    [ApiController]
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class AuthController : TeploController
    {
        private IValidator<User> _validator;
        private readonly IUserService _userService;
        public AuthController( IUserService userService, IValidator<User> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> PostAsync(User user)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors[0].ErrorMessage);
            
            return await _userService.Register(user);
        }

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Post(Login login)
        {
            return await _userService.Authenticate(login);
        }

        /// <summary>
        /// Получение информации о пользователе
        /// </summary>
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> Get()
        {
            return await _userService.GetInformation();
        }
    }
}
