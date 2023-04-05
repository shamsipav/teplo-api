using TeploAPI.Models;

namespace TeploAPI.Interfaces
{
    public interface IFurnaceService
    {
        Task<int> UpdateFurnaceAsync(Furnace furnace);
        Task<int> SaveFurnaceAsync(Furnace furnace, int userId);
    }
}
