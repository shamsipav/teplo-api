﻿using Microsoft.EntityFrameworkCore;
using SweetAPI.Models;
using TeploAPI.Models;

namespace TeploAPI.Data
{
    public class TeploDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Furnace> Furnaces { get; set; }
        public DbSet<Сoefficients> Сoefficients { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Material> Materials { get; set; }

        public TeploDBContext(DbContextOptions<TeploDBContext> options) : base(options) 
        {
            // Фикс ошибки сохранения DateTime в PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Need refactoring?
            Reference reference = Reference.GetDefaultCoefficients();

            modelBuilder.Entity<Сoefficients>().HasData(reference.CokeCunsumptionCoefficents);
            modelBuilder.Entity<Сoefficients>().HasData(reference.FurnanceCapacityCoefficents);

            reference.CokeCunsumptionCoefficents = null;
            reference.FurnanceCapacityCoefficents = null;

            modelBuilder.Entity<Reference>().HasData(reference);

            base.OnModelCreating(modelBuilder);
        }
    }
}
