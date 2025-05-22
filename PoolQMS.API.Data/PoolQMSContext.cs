using Microsoft.EntityFrameworkCore;
using QMSAPI.Models;

namespace PoolQMS.API.Data
{
    public class PoolQMSContext : DbContext
    {
        public PoolQMSContext(DbContextOptions<PoolQMSContext> options)
            : base(options)
        {
        }

        public DbSet<Staff> Staffs { get; set; }
    }
}