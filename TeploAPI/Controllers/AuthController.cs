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
using TeploAPI.Models;
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
                return StatusCode(500, new Response { ErrorMessage = "Не удалось проверить существование пользователя" });
            }

            if (existUser != null)
                return BadRequest(new Response { ErrorMessage = "Пользователь с таким Email уже зарегистрирован" });

            user.Email = user.Email.ToLower();

            // TODO: Remove BCrypt.Net
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = password;

            user.LastLoginIp = HttpContext.Request.Host.ToString();

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Возникло исключение при регистрации пользователя {ex}");
                return StatusCode(500, new Response { ErrorMessage = "Возникла ошибка при регистрации пользователя" });
            }

            try
            {
                var cokeCoefficients = CokeCunsumptionReference.GetDefaultData();
                var furnaceCoefficients = FurnaceCapacityReference.GetDefaultData();

                cokeCoefficients.UserId = user.Id;
                furnaceCoefficients.UserId = user.Id;

                await _context.CokeCunsumptionReferences.AddAsync(cokeCoefficients);
                await _context.FurnanceCapacityReferences.AddAsync(furnaceCoefficients);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Возникло исключение при создании справочника для пользователя {ex}");
            }

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Пользователь успешно зарегистрирован" });
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
                return BadRequest(new Response { ErrorMessage = "Email яляется обязательным полем" });

            if (password == null)
                return BadRequest(new Response { ErrorMessage = "Password яляется обязательным полем" });

            var existUser = _context.Users.FirstOrDefault(u => u.Email == email.ToLower());
            if (existUser is null)
                return NotFound(new Response { ErrorMessage = "Пользователь с таким Email не найден" });

            bool passwordComparison = BCrypt.Net.BCrypt.Verify(password, existUser.Password);
            if (!passwordComparison) return BadRequest(new Response { ErrorMessage = "Неверный пароль" });

            existUser.LastLoginDate = DateTime.Now;
            _context.SaveChanges();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email), new Claim("uid", existUser.Id.ToString()) };

            var timeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: timeNow,
                claims: claims,
                expires: timeNow.Add(TimeSpan.FromDays(AuthOptions.LIFETIMEDAYS)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new Response { IsSuccess = true, SuccessMessage = "Вход в аккаунт выполнен", Result = encodedJwt });
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
                    return NotFound(new Response { ErrorMessage = "Пользователь с таким Email не найден" });
            }
            else
                return BadRequest(new Response { ErrorMessage = "Некорректный токен" });

            return Ok(new Response { IsSuccess = true, 
                Result = new UserDTO {
                    Id = existUser.Id,
                    FirstName = existUser.FirstName,
                    LastName = existUser.LastName,
                    Email = existUser.Email,
                    LastLoginDate = existUser.LastLoginDate,
                    LastLoginIP = existUser.LastLoginIp
             }});
        }
    }
}
