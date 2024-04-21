using TeploAPI.Models;
using TeploAPI.Models.Furnace;
using TeploAPI.ViewModels;

namespace TeploAPI.Interfaces;

public interface ICalculateService
{
    Result СalculateThermalRegime(FurnaceBaseParam input);
    
    ProjectDataViewModel CalculateProjectThermalRegime(FurnaceBaseParam input, FurnaceProjectParam project, Reference reference);
}