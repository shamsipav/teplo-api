using TeploAPI.Models;

namespace TeploAPI.Interfaces;

public interface IMaterialService
{
    /// <summary>
    /// Получить данные по всем материалам в справочнике (для текущего пользователя)
    /// </summary>
    List<Material> GetAllAsync();

    /// <summary>
    /// Добавить материал в справочник (с привязкой к текущему пользователю)
    /// </summary>
    Task<Material> CreateMaterialAsync(Material furnace);

    /// <summary>
    /// Обновить материал
    /// </summary>
    Task<Material> UpdateMaterialAsync(Material furnace);
        
    /// <summary>
    /// Получить данные по конкретному материалу
    /// </summary>
    /// <param name="id">Идентификатор материала</param>
    Task<Material> GetSingleMaterialAsync(Guid id);

    /// <summary>
    /// Удалить материал из справочника
    /// </summary>
    /// <param name="id">Идентификатор материала</param>
    Task<Material> RemoveMaterialAsync(Guid id);
}