using TeploAPI.Models.Furnace;

namespace TeploAPI.Repositories.Interfaces;

public interface IFurnaceRepository
{
    IQueryable<Furnace> GetAll(Guid userId);

    Task<Furnace> GetSingleAsync(Guid id);

    Task<Furnace> AddAsync(Furnace furnace);

    Furnace Update(Furnace furnace);

    Task<Furnace> Delete(Guid id);

    Task<bool> SaveChangesAsync();
}