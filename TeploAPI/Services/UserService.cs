using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TeploAPI.Dtos;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories.Interfaces;
using TeploAPI.Utils;

namespace TeploAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> RegisterAsync(User user)
    {
        User existUser = await _userRepository.GetSingleAsync(user.Email);

        if (existUser != null)
            throw new BusinessLogicException("Пользователь с таким Email уже зарегистрирован");

        user.Email = user.Email.ToLower();

        string hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hashPassword;

        user.LastLoginIp = _httpContextAccessor.HttpContext.Request.Host.ToString();

        user = await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // TODO: Разнести логику ниже по разным сервисам
        var cokeCoefficients = CokeCunsumptionReference.GetDefaultData();
        var furnaceCoefficients = FurnaceCapacityReference.GetDefaultData();

        var defaultFurnaceParam = FurnaceBaseParam.GetDefaultData();
        var defaultFurnace = new Furnace
        {
            NumberOfFurnace = defaultFurnaceParam.NumberOfFurnace,
            UsefulVolumeOfFurnace = defaultFurnaceParam.UsefulVolumeOfFurnace,
            UsefulHeightOfFurnace = defaultFurnaceParam.UsefulHeightOfFurnace,
            DiameterOfColoshnik = defaultFurnaceParam.DiameterOfColoshnik,
            DiameterOfRaspar = defaultFurnaceParam.DiameterOfRaspar,
            DiameterOfHorn = defaultFurnaceParam.DiameterOfHorn,
            HeightOfHorn = defaultFurnaceParam.HeightOfHorn,
            HeightOfTuyeres = defaultFurnaceParam.HeightOfTuyeres,
            HeightOfZaplechiks = defaultFurnaceParam.HeightOfZaplechiks,
            HeightOfRaspar = defaultFurnaceParam.HeightOfRaspar,
            HeightOfShaft = defaultFurnaceParam.HeightOfShaft,
            HeightOfColoshnik = defaultFurnaceParam.HeightOfColoshnik
        };

        cokeCoefficients.UserId = user.Id;
        furnaceCoefficients.UserId = user.Id;
        defaultFurnace.UserId = user.Id;

        // await _context.CokeCunsumptionReferences.AddAsync(cokeCoefficients);
        // await _context.FurnanceCapacityReferences.AddAsync(furnaceCoefficients);
        // await _context.Furnaces.AddAsync(defaultFurnace);
        // await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> AuthenticateAsync(Login login)
    {
        string email = login.Email;
        string password = login.Password;

        if (string.IsNullOrWhiteSpace(email))
            throw new BadRequestException("Email яляется обязательным полем");

        if (string.IsNullOrWhiteSpace(password))
            throw new BadRequestException("Password яляется обязательным полем");

        User existUser = await _userRepository.GetSingleAsync(email.ToLower());
        if (existUser is null)
            throw new NotFoundException("Пользователь с таким Email не найден");

        bool passwordComparison = BCrypt.Net.BCrypt.Verify(password, existUser.Password);
        if (!passwordComparison) 
            throw new BadRequestException("Неверный пароль");

        existUser.LastLoginDate = DateTime.Now;

        _userRepository.Update(existUser);
        await _userRepository.SaveChangesAsync();

        List<Claim> claims = new List<Claim>
            { new Claim(ClaimTypes.Name, email), new Claim(ClaimTypes.NameIdentifier, existUser.Id.ToString()) };

        DateTime timeNow = DateTime.UtcNow;

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: timeNow,
            claims: claims,
            expires: timeNow.Add(TimeSpan.FromDays(AuthOptions.LIFETIMEDAYS)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        string? encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    public async Task<UserDTO> GetInformationAsync()
    {
        IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;

        headers.TryGetValue("Authorization", out var authHeader);

        string token = authHeader.ToString().Split(' ').Skip(1).FirstOrDefault();

        string email = Validate.ValidateToken(token);

        User existUser = new User();

        if (email != null)
        {
            existUser = await _userRepository.GetSingleAsync(email.ToLower());
            if (existUser is null)
                throw new NotFoundException("Пользователь с таким Email не найден");
        }
        else
            throw new BadRequestException("Некорректный токен");

        return new UserDTO
        {
            Id = existUser.Id,
            FirstName = existUser.FirstName,
            LastName = existUser.LastName,
            Email = existUser.Email,
            LastLoginDate = existUser.LastLoginDate,
            LastLoginIP = existUser.LastLoginIp
        };
    }
}