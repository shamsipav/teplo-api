namespace TeploAPI.Models;

/// <summary>
/// Таблица, реализующая связь выбранных шихтовых материалов
/// и вариантов исходных данных / посуточной информации
/// </summary>
public class MaterialsWorkParams
{
    public Guid Id { get; set; }

    public Guid MaterialId { get; set; }
    
    public Guid FurnaceBaseParamId { get; set; }
    
    public double Consumption { get; set; }
}