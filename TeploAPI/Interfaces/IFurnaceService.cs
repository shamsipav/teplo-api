using TeploAPI.Models;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        Task<int> UpdateFurnaceAsync(FurnaceBase furnace);
        Task<int> SaveFurnaceAsync(FurnaceBase furnace, int userId);
    }
}
