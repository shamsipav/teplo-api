using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SweetAPI.Models;
using SweetAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TeploAPI.Data;
using TeploAPI.Utils;

namespace TeploAPI.Controllers
{
    /// <summary>
    /// Авторизация и аутентификация пользователя
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IValidator<User> _validator;
        private readonly TeploDBContext _context;
        public AuthController(TeploDBContext context, IValidator<User> validator)
        {
            _context = context;
            _validator = validator;
        }

        // TODO: try catch
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Объект User</param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<IActionResult> PostAsync(User user)
        {
            Log.Information("POST-запрос на регистрацию");

            ValidationResult validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors[0].ErrorMessage);

            var existUser = new User();
            try
            {
                existUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            }
            catch (Exception ex)
            {
                Log.Error($"POST api/auth/signup, ошибка получения существующего пользователя: {ex}");
            }

            if (existUser != null)
                return BadRequest("Пользователь с таким Email уже зарегистрирован");

            user.Email = user.Email.ToLower();

            // TODO: Remove BCrypt.Net
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = password;

            user.LastLoginIp = HttpContext.Request.Host.ToString();

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error($"Возникло исключение при регистрации пользователя {ex}");
                // TODO: Implement this for Response class
                return Problem("Возникла ошибка при регистрации пользователя");
            }

            return Ok("Пользователь успешно зарегистрирован");
        }

        // TODO: Добавить try/catch в обращения к БД
        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        /// <param name="login">Объект Login</param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Post(Login login)
        {
            Log.Information("POST-запрос на авторизацию");

            var email = login.Email;
            var password = login.Password;

            if (email == null)
                return BadRequest("Email яляется обязательным полем");

            if (password == null)
                return BadRequest("Password яляется обязательным полем");

            var existUser = _context.Users.FirstOrDefault(u => u.Email == email.ToLower());
            if (existUser is null)
                return NotFound("Пользователь с таким Email не найден");

            bool passwordComparison = BCrypt.Net.BCrypt.Verify(password, existUser.Password);
            if (!passwordComparison) return BadRequest("Неверный пароль");

            existUser.LastLoginDate = DateTime.Now;
            _context.SaveChanges();

            var claims = new List<Claim> { new Claim("email", email) };

            var timeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: timeNow,
                claims: claims,
                expires: timeNow.Add(TimeSpan.FromDays(AuthOptions.LIFETIMEDAYS)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }

        /// <summary>
        /// Получение информации о пользователе
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("user")]
        public IActionResult Get()
        {
            Log.Information("GET-запрос на информацию о пользователе");

            var headers = Request.Headers;
            headers.TryGetValue("Authorization", out var authHeader);
            string token = authHeader.ToString().Split(' ').Skip(1).FirstOrDefault();

            string email = Validate.ValidateToken(token);

            var existUser = new User();

            if (email != null)
            {
                existUser = _context.Users.FirstOrDefault(u => u.Email == email.ToLower());
                if (existUser is null)
                    return NotFound("Пользователь с таким Email не найден");
            }
            else
                return BadRequest("Некорректный токен");

            return Ok(new UserDTO
            {
                Id = existUser.Id,
                FirstName = existUser.FirstName,
                LastName = existUser.LastName,
                Email = existUser.Email,
                LastLoginDate = existUser.LastLoginDate,
                LastLoginIP = existUser.LastLoginIp
            });
        }
    }
}
