using Microsoft.EntityFrameworkCore;
using QMSAPI.Dtos.WaterQualityParameter;
using QMSAPI.Models;

namespace QMSAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Staff> Staff { get; set; } // Corrected property name

        public DbSet<Pools> Pools { get; set; } // Added Pools DbSet

        public DbSet<WaterQualityParameter> WaterQualityParameters { get; set; } // Added WaterQualityParameter DbSet

        public DbSet<Chemical> Chemicals { get; set; }

        public DbSet<ChemicalUsageHistory> ChemicalUsageHistory { get; set; }
    }
}
