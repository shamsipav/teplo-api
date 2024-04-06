using TeploAPI.Models;

namespace TeploAPI.Interfaces
{
    public interface IReferenceCoefficientsService
    {
        Task<Reference?> GetCoefficientsReferenceByUserIdAsync(Guid uid);
    }
}
