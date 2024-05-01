using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Interfaces;

public interface IBasePeriodService
{
    /// <summary>
    /// Обработка данных и получение результатов расчета в базовом периоде
    /// </summary>
    Task<ResultViewModel> ProcessBasePeriodAsync(FurnaceBaseParam furnaceBase, bool saveData);

    /// <summary>
    /// Обработка данных и получение результатов расчета в сравнительном периоде
    /// </summary>
    /// <param name="basePeriodId">Идентификатор варианта для расчета базового периода</param>
    /// <param name="comparativePeriodId">Идентификатор варианта для расчета сравнительного периода</param>
    Task<UnionResultViewModel> ProcessComparativePeriodAsync(Guid basePeriodId, Guid comparativePeriodId);
}