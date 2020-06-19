using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PersistenceLayer.Contracts.Models;

namespace PersistenceLayer.Repositories
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<DatabaseDiamond> diamonds { get; set; }
        public virtual DbSet<DatabaseRetailer> retailers { get; set; }

        public DatabaseContext(string connectionString) : this(new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connectionString).Options)
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseDiamond>().ToTable("Diamond").HasKey(x => x.Id);
            modelBuilder.Entity<DatabaseRetailer>().ToTable("Retailer").HasKey(x => x.Id);
        }
    }
}
