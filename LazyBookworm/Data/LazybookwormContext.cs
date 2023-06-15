using System.Collections.Immutable;
using LazyBookworm.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace LazyBookworm.Data;

public partial class LazybookwormContext : DbContext
{
    //public DbSet<Book> Books { get; set; }

    public LazybookwormContext()
    {
    }

    public LazybookwormContext(DbContextOptions<LazybookwormContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
#if DEBUG
            .AddJsonFile("appsettings.Development.json")
#endif
#if RELEASE
            .AddJsonFile("appsettings.json")
#endif
            .Build();
        
        optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
