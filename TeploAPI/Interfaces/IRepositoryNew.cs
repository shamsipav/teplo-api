using System.Linq.Expressions;

namespace TeploAPI.Interfaces
{
    public interface IRepositoryNew<T> where T : class
    {
        /// <summary>
        /// Добавить объект
        /// </summary>
        Task<T> AddAsync(T objModel);

        /// <summary>
        /// Добавить несколько объектов
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> objModel);

        /// <summary>
        /// Получить объект по идентификатору
        /// </summary>
        T? GetById(Guid id);

        /// <summary>
        /// Получить объект по идентификатору (async)
        /// </summary>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить единичный объект, соответствующий лямбда-выражению
        /// </summary>
        T? GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить единичный объект, соответствующий лямбда-выражению (async)
        /// </summary>
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить список объектов, соответствующих лямбда-выражению
        /// </summary>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить список объектов, соответствующих лямбда-выражению (async)
        /// </summary>
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить все объекты с вложенными
        /// </summary>
        public IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Получить все объекты с вложенными, соответствующие лямбда-выражению
        /// </summary>
        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Получить все объекты
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Получить все объекты (async)
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Количество объектов
        /// </summary>
        int Count();

        /// <summary>
        /// Количество объектов (async)
        /// </summary>
        Task<int> CountAsync();

        /// <summary>
        /// Обновить объект
        /// </summary>
        Task<T> UpdateAsync(T objModel);

        /// <summary>
        /// Удалить объект
        /// </summary>
        Task<T?> RemoveAsync(T objModel);

        /// <summary>
        /// Удалить объект
        /// </summary>
        Task<T> RemoveByIdAsync(Guid id);

        void Dispose();
    }
}
