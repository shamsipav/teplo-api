using Microsoft.EntityFrameworkCore;
using TeploAPI.Interfaces;

namespace TeploAPI.Repositories;

public class MainRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly TeploDBContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;
    
    public MainRepository(TeploDBContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public IEnumerable<TEntity> GetAsync(Func<TEntity,bool> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    
    public async Task<TEntity> DeleteAsync(Guid id)
    {
        TEntity? entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        return entity;
    }
    
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}