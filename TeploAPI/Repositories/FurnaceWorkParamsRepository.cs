using Microsoft.EntityFrameworkCore;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories.Interfaces;

namespace TeploAPI.Repositories;

public class FurnaceWorkParamsRepository : IFurnaceWorkParamsRepository
{
    private readonly TeploDBContext _dbContext;
    
    public FurnaceWorkParamsRepository(TeploDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<FurnaceBaseParam> GetAll(Guid userId, bool isDaily = false)
    {
        IQueryable<FurnaceBaseParam> baseParams = _dbContext.FurnacesWorkParams
                                                                  .AsNoTracking()
                                                                  .Include(i => i.MaterialsWorkParamsList)
                                                                  .Where(v => v.UserId.Equals(userId))
                                                                  .OrderBy(v => v.SaveDate);

        if (isDaily)
            baseParams = baseParams.Where(p => p.Day != DateTime.MinValue);

        return baseParams;
    }

    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id)
    {
        return await _dbContext.FurnacesWorkParams.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
    
    public async Task<FurnaceBaseParam> GetSingleAsync(Guid id, DateTime day)
    {
        return await _dbContext.FurnacesWorkParams.FirstOrDefaultAsync(f => f.FurnaceId.Equals(id) && f.Day == day);
    }

    public async Task<FurnaceBaseParam> AddAsync(FurnaceBaseParam furnaceBaseParam)
    {
        await _dbContext.FurnacesWorkParams.AddAsync(furnaceBaseParam);
        return furnaceBaseParam;
    }
    
    public FurnaceBaseParam Update(FurnaceBaseParam furnaceBaseParam)
    {
        _dbContext.FurnacesWorkParams.Update(furnaceBaseParam);
        return furnaceBaseParam;
    }
    
    public async Task<FurnaceBaseParam> Delete(Guid id)
    {
        FurnaceBaseParam baseParam = await GetSingleAsync(id);
        _dbContext.FurnacesWorkParams.Remove(baseParam);

        return baseParam;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}