using Microsoft.AspNetCore.Mvc;
using SweetAPI.Models;
using TeploAPI.Models;

namespace TeploAPI.Interfaces;

public interface IUserService
{
    Task<ObjectResult> RegisterAsync(User user);
    
    Task<ObjectResult> AuthenticateAsync(Login login);
    
    Task<ObjectResult> GetInformationAsync();
}