using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services
{
    public class FurnaceService : IFurnaceService
    {
        private readonly IRepository<Furnace> _furnaceRepository;
        private readonly IRepository<FurnaceBaseParam> _variantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<Furnace> _validator;

        public FurnaceService(IRepository<Furnace> furnaceRepository, IHttpContextAccessor httpContextAccessor, 
            IValidator<Furnace> validator, IRepository<FurnaceBaseParam> variantRepository)
        {
            _furnaceRepository = furnaceRepository;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _variantRepository = variantRepository;
        }

        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        public List<Furnace> GetAll()
        {
            Guid userId = _user.GetUserId();
            return _furnaceRepository.Get(f => f.UserId == userId).ToList();
        }

        public async Task<Furnace> CreateFurnaceAsync(Furnace furnace)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnace);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors[0].ErrorMessage);
            
            furnace.UserId = _user.GetUserId();

            await _furnaceRepository.AddAsync(furnace);

            return furnace;
        }

        public async Task<Furnace> UpdateFurnaceAsync(Furnace furnace)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(furnace);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors[0].ErrorMessage);
            
            Furnace existFurnace = await _furnaceRepository.GetByIdAsync(furnace.Id);

            if (existFurnace == null)
                throw new BusinessLogicException($"Не удалось найти информацию о печи с идентификатором id = '{furnace.Id}'");

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

            await _furnaceRepository.UpdateAsync(furnace);

            return furnace;
        }

        public async Task<Furnace> GetSingleFurnaceAsync(Guid id)
        {
            Furnace furnace = await _furnaceRepository.GetByIdAsync(id);

            if (furnace == null)
                throw new BusinessLogicException($"Не удалось найти печь с идентификатором {id}");

            return furnace;
        }

        public async Task<Furnace> RemoveFurnaceAsync(Guid id)
        {
            FurnaceBaseParam variantWithThisFurnace = _variantRepository.GetSingle(v => v.FurnaceId == id);

            if (variantWithThisFurnace != null)
                throw new BusinessLogicException($"На данную печь ссылается вариант исходных данных или посуточная информация о работе ДП");

            Furnace deletedFurnace = await _furnaceRepository.RemoveByIdAsync(id);

            return deletedFurnace;
        }
    }
}