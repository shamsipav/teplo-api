using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.ViewModels
{
    /// <summary>
    /// Класс для отображение исходных данных и результатов расчета
    /// </summary>
    public class ResultViewModel
    {
        public FurnaceBaseParam? Input { get; set; }
        public Result? Result { get; set; }
    }
}
