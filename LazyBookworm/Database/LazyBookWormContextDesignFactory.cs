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
#if DEBUG
            var connectionString = $"Host=localhost;Port=3306;Database=LazyBookworm;Username=test;Password=test;Convert Zero Datetime=True";

#endif
#if RELEASE
            var connectionString = $"Host={Settings.DatabaseSettings.DatabaseHost};Port=3306;Database={Settings.DatabaseSettings.DatabaseName};Username={Settings.DatabaseSettings.DatabaseUser};Password={Settings.DatabaseSettings.DatabasePassword};Convert Zero Datetime=True";
#endif

            var optionsBuilder = new DbContextOptionsBuilder<LazyBookWormContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new LazyBookWormContext(optionsBuilder.Options);
        }
    }
}