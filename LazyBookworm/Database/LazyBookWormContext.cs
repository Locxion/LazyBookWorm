using LazyBookworm.Common.Models;
using LazyBookworm.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LazyBookworm.Database
{
    public class LazyBookWormContext : DbContext
    {
        public DbSet<UserAccount> Accounts { get; set; }

        public LazyBookWormContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        }
    }
}