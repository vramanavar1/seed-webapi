using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PersistenceLayer.Contracts.Models;

namespace PersistenceLayer.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseDiamond> diamonds { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            //    _ = optionsBuilder.UseInMemoryDatabase("DiamondsDB");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseDiamond>().ToTable("Diamond").HasKey(x => x.Id);
            modelBuilder.Entity<DatabaseRetailer>().ToTable("Retailer").HasKey(x => x.Id);
        }
    }
}
