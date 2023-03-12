using TeploAPI.Models;

namespace TeploAPI.ViewModels
{
    /// <summary>
    /// Класс для отображение исходных данных и результатов расчета
    /// </summary>
    public class ResultViewModel
    {
        public Furnace? Input { get; set; }
        public Result? Result { get; set; }
    }
}
