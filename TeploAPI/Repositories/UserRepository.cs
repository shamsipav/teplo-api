using Microsoft.EntityFrameworkCore;
using TeploAPI.Data.Interfaces;
using TeploAPI.Models;

namespace TeploAPI.Data;

public class UserRepository : IUserRepository
{
    private readonly TeploDBContext _dbContext;
    
    public UserRepository(TeploDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // public async Task<User> GetAll()
    // {
    //     return new NotImplementedException();
    // }

    public async Task<User> GetSingleAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
    
    public async Task<User> GetSingleAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        return user;
    }
    
    public User Update(User user)
    {
         _dbContext.Users.Update(user);
         return user;
    }
    
    public async Task Delete(Guid id)
    {
        User user = await GetSingleAsync(id);
        _dbContext.Users.Remove(user);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}