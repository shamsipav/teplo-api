using TeploAPI.Models;

namespace TeploAPI.Data.Interfaces;

public interface IUserRepository
{
    public Task<User> GetSingleAsync(Guid id);

    public Task<User> GetSingleAsync(string email);

    public Task<User> AddAsync(User user);

    public User Update(User user);

    public Task Delete(Guid id);

    public Task<bool> SaveChangesAsync();
}