using TeploAPI.Models;

namespace TeploAPI.Interfaces
{
    public interface IReferenceCoefficientsService
    {
        /// <summary>
        /// Получение значений справочника корректировочных коэффициентов для текущего пользователя
        /// </summary>
        Task<Reference> GetCoefficientsReference();

        /// <summary>
        /// Обновление значений справочника корректировочных коэффициентов для текущего пользователя
        /// </summary>
        Task<Reference> UpdateCoefficientsReference(Reference reference);
    }
}
