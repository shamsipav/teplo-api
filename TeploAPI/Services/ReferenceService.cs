using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Interfaces;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Services
{
    public class ReferenceService: IReferenceCoefficientsService
    {
        private TeploDBContext _context;
        public ReferenceService(TeploDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение значений для справочника корректировочных коэффициентов
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<Reference?> GetCoefficientsReferenceByUserIdAsync(Guid uid)
        {
            var cokeCoefficients = new CokeCunsumptionReference();
            var furnanceCapacityCoefficients = new FurnaceCapacityReference();

            try
            {
                cokeCoefficients = await _context.CokeCunsumptionReferences.AsNoTracking().FirstOrDefaultAsync(c => c.UserId.Equals(uid));
                furnanceCapacityCoefficients = await _context.FurnanceCapacityReferences.AsNoTracking().FirstOrDefaultAsync(f => f.UserId.Equals(uid));

                return new Reference { CokeCunsumptionReference = cokeCoefficients, FurnaceCapacityReference = furnanceCapacityCoefficients };
            }
            catch (Exception ex)
            {
                Log.Error($"HTTP PUT api/reference GetAsync: Ошибка получения коэффициентов для справочника: {ex}");
                return null;
            }
        }
    }
}
