using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeploAPI.Interfaces;

namespace TeploAPI.Repositories
{
    public class MainRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TeploDBContext _context;

        public MainRepository(TeploDBContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> model)
        {
            _context.Set<TEntity>().AddRange(model);
            await _context.SaveChangesAsync();
        }

        public TEntity? GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool isTracking = true)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity != null && !isTracking)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).AsEnumerable();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity objModel)
        {
            _context.Entry(objModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return objModel;
        }

        public async Task<TEntity?> RemoveAsync(TEntity objModel)
        {
            _context.Set<TEntity>().Remove(objModel);
            await _context.SaveChangesAsync();

            return objModel;
        }

        public async Task<TEntity> RemoveByIdAsync(Guid id)
        {
            TEntity? entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
