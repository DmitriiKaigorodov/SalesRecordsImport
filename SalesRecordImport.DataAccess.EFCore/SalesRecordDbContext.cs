using Microsoft.EntityFrameworkCore;
using SalesRecordImport.Domain;

namespace SalesRecordImport.DataAccess.EFCore
{
    public class SalesRecordDbContext : DbContext
    {
        public DbSet<SalesRecord> SalesRecords { get; set; }

        public SalesRecordDbContext(DbContextOptions<SalesRecordDbContext> options) : base(options)
        {
            base.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SalesRecord>()
                .HasIndex(x => x.Country);

            builder.Entity<SalesRecord>()
                .HasIndex(x => x.OrderDate);

            builder.Entity<SalesRecord>()
                .Property(x => x.TotalCost)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.TotalCost)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.TotalProfit)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.TotalRevenue)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.UnitCost)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.UnitPrice)
                .HasColumnType("money");

            builder.Entity<SalesRecord>()
                .Property(x => x.UnitsSold)
                .HasColumnType("money");

        }
    }
}