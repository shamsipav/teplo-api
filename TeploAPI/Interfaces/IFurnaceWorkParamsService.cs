using TeploAPI.Models.Furnace;

namespace TeploAPI.Interfaces;

public interface IFurnaceWorkParamsService
{
    List<FurnaceBaseParam> GetAll(bool isDaily);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id);

    FurnaceBaseParam GetSingle(Guid id, DateTime day);

    Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam);

    Task<FurnaceBaseParam> UpdateAsync(FurnaceBaseParam furnace);

    Task<FurnaceBaseParam> CreateOrUpdateAsync(FurnaceBaseParam dailyInfo);

    Task<FurnaceBaseParam> RemoveAsync(Guid id);
}