using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        Task<Guid> UpdateFurnaceAsync(FurnaceBaseParam furnace);
        Task<Guid> SaveFurnaceAsync(FurnaceBaseParam furnace, Guid userId);
    }
}
