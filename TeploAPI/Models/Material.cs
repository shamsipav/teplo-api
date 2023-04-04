namespace TeploAPI.Models
{
    public class Material
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название материала
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Расход, кг/т чугуна
        /// </summary>
        public double Coal { get; set; }

        /// <summary>
        /// Доля
        /// </summary>
        public double Share { get; set; }

        /// <summary>
        /// Содержание влаги, %
        /// </summary>
        public double Moisture { get; set; }

        /// <summary>
        /// Содержание Fe2O3, %
        /// </summary>
        public double Fe2O3 { get; set; }

        /// <summary>
        /// Содержание Fe, %
        /// </summary>
        public double Fe { get; set; }

        /// <summary>
        /// Содержание FeO, %
        /// </summary>
        public double FeO { get; set; }

        /// <summary>
        /// Содержание CaO, %
        /// </summary>
        public double CaO { get; set; }

        /// <summary>
        /// Содержание SiO2, %
        /// </summary>
        public double SiO2 { get; set; }

        /// <summary>
        /// Содержание MgO, %
        /// </summary>
        public double MgO { get; set; }

        /// <summary>
        /// Содержание Al2O3, %
        /// </summary>
        public double Al2O3 { get; set; }

        /// <summary>
        /// Содержание TiO2, %
        /// </summary>
        public double TiO2 { get; set; }

        /// <summary>
        /// Содержание MnO, %
        /// </summary>
        public double MnO { get; set; }

        /// <summary>
        /// Содержание P, %
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// Содержание S, %
        /// </summary>
        public double S { get; set; }

        /// <summary>
        /// Содержание Zn, %
        /// </summary>
        public double Zn { get; set; }

        /// <summary>
        /// Содержание Mn, %
        /// </summary>
        public double Mn { get; set; }

        /// <summary>
        /// Содержание Cr, %
        /// </summary>
        public double Cr { get; set; }

        /// <summary>
        /// Содержание 5-0мм, %
        /// </summary>
        public double FiveZero { get; set; }

        /// <summary>
        /// Содержание Осн1, %
        /// </summary>
        public double BaseOne { get; set; }
    }
}
