using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        Task<List<Furnace>> GetAll(Guid userId);

        Task<Furnace> CreateAsync(Furnace furnace, Guid userId);

        Task<Furnace> UpdateAsync(Furnace furnace);
        
        Task<Furnace> GetSingleAsync(Guid id);
        
        // Task<Guid> UpdateFurnaceAsync(FurnaceBaseParam furnace);
        //
        // Task<Guid> SaveFurnaceAsync(FurnaceBaseParam furnace, Guid userId);

        Task<Furnace> RemoveFurnaceAsync(Guid id);
    }
}
