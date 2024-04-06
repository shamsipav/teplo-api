using TeploAPI.Models.Furnace;

namespace TeploAPI.ViewModels
{
    public class ProjectDataViewModel
    {
        public FurnaceBaseParam? ChangedInputData { get; set; }
        public FurnaceProjectParam? ProjectInputData { get; set; }
    }
}
