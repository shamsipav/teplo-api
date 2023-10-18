using Microsoft.EntityFrameworkCore;
using SweetAPI.Models;
using TeploAPI.Models;

namespace TeploAPI.Data
{
    public class TeploDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Furnace> Furnaces { get; set; }
        public DbSet<FurnaceBase> FurnaceBases { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CokeCunsumptionReference> CokeCunsumptionReferences { get; set; }
        public DbSet<FurnaceCapacityReference> FurnanceCapacityReferences { get; set; }

        public TeploDBContext(DbContextOptions<TeploDBContext> options) : base(options) 
        {
            // Фикс ошибки сохранения DateTime в PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
