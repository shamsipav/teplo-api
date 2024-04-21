using TeploAPI.Dtos;
using TeploAPI.Models;

namespace TeploAPI.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(User user);
    
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <returns>JWT-токен</returns>
    Task<string> AuthenticateAsync(Login login);
    
    Task<UserDTO> GetInformationAsync();
}