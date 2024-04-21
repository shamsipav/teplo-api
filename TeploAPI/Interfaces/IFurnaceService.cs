using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        /// <summary>
        /// Получить данные по всем печам в справочнике (для текущего пользователя)
        /// </summary>
        Task<List<Furnace>> GetAll();

        /// <summary>
        /// Добавить данные печи в справочник (с привязкой к текущему пользователю)
        /// </summary>
        Task<Furnace> CreateFurnaceAsync(Furnace furnace);

        /// <summary>
        /// Обновить данные печи
        /// </summary>
        Task<Furnace> UpdateFurnaceAsync(Furnace furnace);
        
        /// <summary>
        /// Получить данные по конкретной печи
        /// </summary>
        /// <param name="id">Идентификатор печи</param>
        Task<Furnace> GetSingleFurnaceAsync(Guid id);

        /// <summary>
        /// Удалить данные из справочника печей
        /// </summary>
        /// <param name="id">Идентификатор печи</param>
        Task<Furnace> RemoveFurnaceAsync(Guid id);
    }
}
