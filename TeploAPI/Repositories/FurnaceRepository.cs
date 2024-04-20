using Microsoft.EntityFrameworkCore;
using TeploAPI.Models.Furnace;
using TeploAPI.Repositories.Interfaces;

namespace TeploAPI.Repositories;

public class FurnaceRepository : IFurnaceRepository
{
    private readonly TeploDBContext _dbContext;
    
    public FurnaceRepository(TeploDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<Furnace> GetAll(Guid userId)
    {
        IQueryable<Furnace> furnaces = _dbContext.Furnaces.AsNoTracking().Where(m => m.UserId.Equals(userId));

        return furnaces;
    }

    public async Task<Furnace> GetSingleAsync(Guid id)
    {
        return await _dbContext.Furnaces.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Furnace> AddAsync(Furnace furnace)
    {
        await _dbContext.Furnaces.AddAsync(furnace);
        return furnace;
    }
    
    public Furnace Update(Furnace furnace)
    {
        _dbContext.Furnaces.Update(furnace);
        return furnace;
    }
    
    public async Task<Furnace> Delete(Guid id)
    {
        Furnace furnace = await GetSingleAsync(id);
        _dbContext.Furnaces.Remove(furnace);

        return furnace;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}