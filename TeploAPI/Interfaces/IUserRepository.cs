using TeploAPI.Models;

namespace TeploAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetSingleAsync(Guid id);

    Task<User> GetSingleAsync(string email);

    Task<User> AddAsync(User user);

    User Update(User user);

    Task Delete(Guid id);

    Task<bool> SaveChangesAsync();
}