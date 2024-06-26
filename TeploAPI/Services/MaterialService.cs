﻿using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using TeploAPI.Exceptions;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.Utils.Extentions;

namespace TeploAPI.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<FurnaceBaseParam> _variantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<Material> _validator;

        public MaterialService(IRepository<Material> materialRepository, IHttpContextAccessor httpContextAccessor, 
            IValidator<Material> validator, IRepository<FurnaceBaseParam> variantRepository)
        {
            _materialRepository = materialRepository;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _variantRepository = variantRepository;
        }

        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        public List<Material> GetAll()
        {
            Guid userId = _user.GetUserId();

            return _materialRepository.Get(f => f.UserId == userId).ToList();
        }

        public async Task<Material> CreateMaterialAsync(Material material)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(material);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors[0].ErrorMessage);
            
            material.UserId = _user.GetUserId();
            material.BaseOne = material.CaO / material.SiO2;

            await _materialRepository.AddAsync(material);

            return material;
        }

        public async Task<Material> UpdateMaterialAsync(Material material)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(material);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors[0].ErrorMessage);
            
            Material existMaterial = await _materialRepository.GetByIdAsync(material.Id);

            if (existMaterial == null)
                throw new BusinessLogicException($"Не удалось найти информацию о материале с идентификатором id = '{material.Id}'");

            existMaterial.Name = material.Name;
            existMaterial.Moisture = material.Moisture;
            existMaterial.Fe2O3 = material.Fe2O3;
            existMaterial.Fe = material.Fe;
            existMaterial.FeO = material.FeO;
            existMaterial.CaO = material.CaO;
            existMaterial.SiO2 = material.SiO2;
            existMaterial.MgO = material.MgO;
            existMaterial.Al2O3 = material.Al2O3;
            existMaterial.TiO2 = material.TiO2;
            existMaterial.MnO = material.MnO;
            existMaterial.P = material.P;
            existMaterial.S = material.S;
            existMaterial.Zn = material.Zn;
            existMaterial.Mn = material.Mn;
            existMaterial.Cr = material.Cr;
            existMaterial.FiveZero = material.FiveZero;
            existMaterial.BaseOne = material.CaO / material.SiO2;

            await _materialRepository.UpdateAsync(existMaterial);

            return existMaterial;
        }

        public async Task<Material> GetSingleMaterialAsync(Guid id)
        {
            Material material = await _materialRepository.GetByIdAsync(id);

            if (material == null)
                throw new BusinessLogicException($"Не удалось найти материал с идентификатором '{id}'");

            return material;
        }

        public async Task<Material> RemoveMaterialAsync(Guid id)
        {
            // Выборка вариантов исходных данных или посуточной информации о работе ДП
            // где присутствует удаляемый материал
            List<MaterialsWorkParams> existBaseParam = _variantRepository
                                                       .GetWithInclude(p => p.MaterialsWorkParamsList)
                                                       .SelectMany(x => x.MaterialsWorkParamsList)
                                                       .Where(m => m.MaterialId == id)
                                                       .ToList();

            if (existBaseParam != null && existBaseParam.Any())
                throw new BusinessLogicException($"На данный материал ссылается вариант исходных данных или посуточная информация о работе ДП");

            Material deletedMaterial = await _materialRepository.RemoveByIdAsync(id);

            return deletedMaterial;
        }
    }
}