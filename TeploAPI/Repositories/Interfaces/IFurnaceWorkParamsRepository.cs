using TeploAPI.Models.Furnace;

namespace TeploAPI.Repositories.Interfaces;

public interface IFurnaceWorkParamsRepository
{
    IQueryable<FurnaceBaseParam> GetAll(Guid userId, bool isDaily);

    Task<FurnaceBaseParam> GetSingleAsync(Guid id);
    
    Task<FurnaceBaseParam> GetSingleAsync(Guid id, DateTime day);

    Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam);

    FurnaceBaseParam Update(FurnaceBaseParam furnaceBaseParam);

    Task<FurnaceBaseParam> Delete(Guid id);

    Task<bool> SaveChangesAsync();
}