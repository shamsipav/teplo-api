using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces;

public interface IFurnaceWorkParamsService
{
    Task<List<FurnaceBaseParam>> GetAll(Guid userId, bool isDaily);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id, DateTime day);

    Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam, Guid userId);

    Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam furnace);

    Task<FurnaceBaseParam> CreateOrUpdateAsync(FurnaceBaseParam dailyInfo, Guid userId);

    Task<FurnaceBaseParam> RemoveAsync(Guid id);
}