namespace TeploAPI.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<TEntity> AddAsync(TEntity furnace);

    Task<TEntity> UpdateAsync(TEntity furnace);

    Task<TEntity> DeleteAsync(Guid id);

    Task SaveChangesAsync();
}