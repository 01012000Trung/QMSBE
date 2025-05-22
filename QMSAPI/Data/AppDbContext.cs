using Microsoft.EntityFrameworkCore;
using QMSAPI.Models;

namespace QMSAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Staff> Staff { get; set; } // Corrected property name
    }
}
