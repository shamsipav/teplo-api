﻿namespace TeploAPI.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Получить все объекты
    /// </summary>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Получить все объекты, соответствующие лямбда-выражению
    /// </summary>
    IQueryable<TEntity> Get(Func<TEntity, bool> predicate);

    /// <summary>
    /// Получить первый попавшийся объект, соответствующий лямбда-выражению
    /// </summary>
    TEntity? GetSingle(Func<TEntity, bool> predicate);

    /// <summary>
    /// Получение объекта по его идентификатору
    /// </summary>
    Task<TEntity> GetByIdAsync(Guid id);

    /// <summary>
    /// Создание объекта
    /// </summary>
    Task<TEntity> AddAsync(TEntity furnace);

    /// <summary>
    /// Редактирование объекта
    /// </summary>
    Task<TEntity> UpdateAsync(TEntity furnace);

    /// <summary>
    /// Удаление объекта
    /// </summary>
    Task<TEntity> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранение изменений
    /// </summary>
    Task SaveChangesAsync();
}