using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces;

public interface IFurnaceWorkParamsService
{
    Task<List<FurnaceBaseParam>> GetAllAsync( bool isDaily);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id, DateTime day);

    Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam);

    Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam furnace);

    Task<FurnaceBaseParam> CreateOrUpdateAsync(FurnaceBaseParam dailyInfo);

    Task<FurnaceBaseParam> RemoveAsync(Guid id);
}