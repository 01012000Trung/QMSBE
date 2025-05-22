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

        public DbSet<Staff> Staff { get; set; }
        //public DbSet<Pool> Pools { get; set; }
        //public DbSet<Chemical> Chemicals { get; set; }
        //public DbSet<WaterQualityRecord> WaterQualityRecords { get; set; }

        // Cấu hình và dữ liệu mẫu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tạo dữ liệu mẫu
            // Quan hệ giữa các bảng
        }
    }
}