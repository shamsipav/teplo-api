namespace TeploAPI.Models.Furnace
{
    /// <summary>
    /// Класс, описывающий статические характеристики доменной печи
    /// Нужен для реализации справочника доменных печей
    /// </summary>
    public class Furnace : FurnaceBase
    {
        public Guid Id { get; set; }

        // TODO: Возможно, стоит реализовать более корректную связь, чтобы
        // другой пользователь не мог получить варианты исходных данных у данного пользователя.
        public Guid UserId { get; set; }
    }
}
