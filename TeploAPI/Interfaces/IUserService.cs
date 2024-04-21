using TeploAPI.Dtos;
using TeploAPI.Models;

namespace TeploAPI.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Создание аккаунта
    /// </summary>
    Task<User> RegisterAsync(User user);
    
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <returns>JWT-токен</returns>
    Task<string> AuthenticateAsync(Login login);
    
    /// <summary>
    /// Получение информации о текущем пользователе
    /// </summary>
    Task<UserDTO> GetInformationAsync();
}