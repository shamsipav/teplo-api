using Microsoft.AspNetCore.Mvc;
using SweetAPI.Models;
using TeploAPI.Models;

namespace TeploAPI.Interfaces;

public interface IUserService
{
    public Task<ObjectResult> Register(User user);
    
    public Task<ObjectResult> Authenticate(Login login);
    
    public Task<ObjectResult> GetInformation();
}