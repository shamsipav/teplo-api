using Microsoft.EntityFrameworkCore;
using TeploAPI.Models;

namespace TeploAPI.Data
{
    public class TeploDBContext : DbContext
    {
        public DbSet<Furnace> Furnaces { get; set; }

        public TeploDBContext(DbContextOptions<TeploDBContext> options) : base(options) { }
    }
}
