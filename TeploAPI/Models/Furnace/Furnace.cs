namespace TeploAPI.Models.Furnace
{
    /// <summary>
    /// Класс, описывающий статические характеристики доменной печи
    /// Нужен для реализации справочника доменных печей
    /// </summary>
    public class Furnace : FurnaceBase
    {
        public Guid Id { get; set; }

        // TODO: Возможно, стоит реализовать более корректную связь
        public Guid UserId { get; set; }
    }
}
