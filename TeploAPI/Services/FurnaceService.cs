using System.Security.Claims;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services
{
    public class FurnaceService : IFurnaceService
    {
        private readonly IRepository<Furnace> _furnaceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FurnaceService(IRepository<Furnace> furnaceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _furnaceRepository = furnaceRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        
        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        public List<Furnace> GetAll()
        {
            Guid userId = _user.GetUserId();
            return _furnaceRepository.Get(f => f.UserId == userId).ToList();
        }

        public async Task<Furnace> CreateFurnaceAsync(Furnace furnace)
        {
            furnace.UserId = _user.GetUserId();

            await _furnaceRepository.AddAsync(furnace);
            await _furnaceRepository.SaveChangesAsync();

            return furnace;
        }

        public async Task<Furnace> UpdateFurnaceAsync(Furnace furnace)
        {
            Furnace existFurnace = await _furnaceRepository.GetByIdAsync(furnace.Id);

            if (existFurnace == null)
                throw new NoContentException($"Не удалось найти информацию о печи с идентификатором id = '{furnace.Id}'");

            existFurnace.NumberOfFurnace = furnace.NumberOfFurnace;
            existFurnace.UsefulVolumeOfFurnace = furnace.UsefulVolumeOfFurnace;
            existFurnace.UsefulHeightOfFurnace = furnace.UsefulHeightOfFurnace;
            existFurnace.DiameterOfColoshnik = furnace.DiameterOfColoshnik;
            existFurnace.DiameterOfRaspar = furnace.DiameterOfRaspar;
            existFurnace.DiameterOfHorn = furnace.DiameterOfHorn;
            existFurnace.HeightOfHorn = furnace.HeightOfHorn;
            existFurnace.HeightOfTuyeres = furnace.HeightOfTuyeres;
            existFurnace.HeightOfZaplechiks = furnace.HeightOfZaplechiks;
            existFurnace.HeightOfRaspar = furnace.HeightOfRaspar;
            existFurnace.HeightOfShaft = furnace.HeightOfShaft;
            existFurnace.HeightOfColoshnik = furnace.HeightOfColoshnik;

            _furnaceRepository.UpdateAsync(furnace);
            await _furnaceRepository.SaveChangesAsync();

            return furnace;
        }

        public async Task<Furnace> GetSingleFurnaceAsync(Guid id)
        {
            Furnace furnace = await _furnaceRepository.GetByIdAsync(id);

            if (furnace == null)
                throw new NoContentException($"Не удалось найти печь с идентификатором {id}");

            return furnace;
        }
        
        public async Task<Furnace> RemoveFurnaceAsync(Guid id)
        {
            Furnace deletedFurnace = await _furnaceRepository.DeleteAsync(id);
            await _furnaceRepository.SaveChangesAsync();

            return deletedFurnace;
        }
    }
}