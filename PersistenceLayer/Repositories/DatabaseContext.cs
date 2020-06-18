using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Contracts.Models;

namespace PersistenceLayer.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Diamond> diamonds { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
