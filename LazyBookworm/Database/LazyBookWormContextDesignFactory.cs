using LazyBookworm.Models.Common;
using LazyBookworm.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LazyBookworm.Database
{
    public class LazyBookWormContextDesignFactory : IDesignTimeDbContextFactory<LazyBookWormContext>
    {
        private Settings Settings;

        public LazyBookWormContextDesignFactory()
        {
            Settings = SettingsService.LoadSettings();
        }

        public LazyBookWormContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();

            var connectionString = $"Host={Settings.DatabaseHost};Port=3306;Database={Settings.DatabaseName};Username={Settings.DatabaseUser};Password={Settings.DatabasePassword};Convert Zero Datetime=True";

            var optionsBuilder = new DbContextOptionsBuilder<LazyBookWormContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new LazyBookWormContext(optionsBuilder.Options);
        }
    }
}