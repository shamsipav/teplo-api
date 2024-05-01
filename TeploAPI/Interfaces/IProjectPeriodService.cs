using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Interfaces;

public interface IProjectPeriodService
{
    /// <summary>
    /// Обработка данных и получение результатов расчета в проектном периоде
    /// </summary>
    /// <param name="projectPeriodFurnaceData">Данные для расчета проектного периода</param>
    /// <param name="inputDataId">Идентификатор варианта исходных данных</param>
    /// <returns></returns>
    Task<UnionResultViewModel> ProcessProjectPeriod(FurnaceProjectParam projectPeriodFurnaceData, Guid inputDataId);
}