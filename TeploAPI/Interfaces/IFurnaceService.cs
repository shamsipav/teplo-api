using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        Task<Guid> UpdateFurnaceAsync(FurnaceBaseParam furnace, bool isDaily = false);
        Task<Guid> SaveFurnaceAsync(FurnaceBaseParam furnace, Guid userId);
    }
}
