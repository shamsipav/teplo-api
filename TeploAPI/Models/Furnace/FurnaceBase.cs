namespace TeploAPI.Models.Furnace
{
    /// <summary>
    ///  Класс, содержащий описание только статичный характеристик доменных печах
    /// </summary>
    public abstract class FurnaceBase
    {
        /// <summary>
        /// Номер доменной печи
        /// </summary>
        public int NumberOfFurnace { get; set; }

        /// <summary>
        /// Полезный объем печи, м3
        /// </summary>
        public double UsefulVolumeOfFurnace { get; set; }
        /// <summary>
        /// Полезная высота печи, мм
        /// </summary>
        public double UsefulHeightOfFurnace { get; set; }

        /// <summary>
        /// Диаметр колошника, мм
        /// </summary>
        public double DiameterOfColoshnik { get; set; }
        /// <summary>
        /// Диаметр распара, мм
        /// </summary>
        public double DiameterOfRaspar { get; set; }
        /// <summary>
        /// Диаметр горна, мм
        /// </summary>
        public double DiameterOfHorn { get; set; }

        /// <summary>
        /// Высота горна, мм
        /// </summary>
        public double HeightOfHorn { get; set; }
        /// <summary>
        /// Высота фурм, мм
        /// </summary>
        public double HeightOfTuyeres { get; set; }
        /// <summary>
        /// Высота заплечников, мм
        /// </summary>
        public double HeightOfZaplechiks { get; set; }
        /// <summary>
        /// Высота распара, мм
        /// </summary>
        public double HeightOfRaspar { get; set; }
        /// <summary>
        /// Высота шахты, мм
        /// </summary>
        public double HeightOfShaft { get; set; }
        /// <summary>
        /// Высота колошника, мм
        /// </summary>
        public double HeightOfColoshnik { get; set; }
    }
}
