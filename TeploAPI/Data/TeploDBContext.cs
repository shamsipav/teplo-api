using Microsoft.EntityFrameworkCore;
using SweetAPI.Models;
using TeploAPI.Models;
using TeploAPI.Models.Furnace;

namespace TeploAPI.Data
{
    public class TeploDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Справочник доменных печей
        /// </summary>
        public DbSet<Furnace> Furnaces { get; set; }
        /// <summary>
        /// Варианты исходных данных + посуточная информация о работе доменных печей (FurnaceBase)
        /// На текущий момент разделение происходит путём анализа наличия значения у свойства Day
        /// </summary>
        public DbSet<FurnaceBaseParam> FurnacesWorkParams { get; set; }
        /// <summary>
        /// Справочных шихтовых материалов
        /// </summary>
        public DbSet<Material> Materials { get; set; }
        public DbSet<CokeCunsumptionReference> CokeCunsumptionReferences { get; set; }
        public DbSet<FurnaceCapacityReference> FurnanceCapacityReferences { get; set; }

        public TeploDBContext(DbContextOptions<TeploDBContext> options) : base(options) 
        {
            // Фикс ошибки сохранения DateTime в PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            // Rider EF commands:
            // 1. dotnet ef add-migration [name]
            // 2. dotnet ef database update [migration_name]
        }
    }
}
