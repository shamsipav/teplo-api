using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeploAPI.Dtos;
using TeploAPI.Filters;
using TeploAPI.Interfaces;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    /// <summary>
    /// Аутентификация и авторизация пользователя
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> PostAsync(User user)
        {
            await _userService.RegisterAsync(user);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Пользователь успешно зарегистрирован" });
        }

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Post(Login login)
        {
            string encodedJwt = await _userService.AuthenticateAsync(login);
            
            return Ok(new Response { IsSuccess = true, SuccessMessage = "Вход в аккаунт выполнен", Result = encodedJwt });
        }

        /// <summary>
        /// Получение информации о пользователе
        /// </summary>
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> Get()
        {
            UserDTO userInfo = await _userService.GetInformationAsync();

            return Ok(new Response { IsSuccess = true, Result = userInfo });
        }
    }
}
