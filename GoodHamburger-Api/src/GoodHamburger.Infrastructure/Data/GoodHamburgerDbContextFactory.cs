using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GoodHamburger.Infrastructure.Data;

public class GoodHamburgerDbContextFactory : IDesignTimeDbContextFactory<GoodHamburgerDbContext>
{
    public GoodHamburgerDbContext CreateDbContext(string[] args)
    {
        // Get directory of the startup project (API)
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "GoodHamburger.API");
        
        // Fallback for different execution contexts
        if (!Directory.Exists(basePath))
        {
             basePath = Directory.GetCurrentDirectory();
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var builder = new DbContextOptionsBuilder<GoodHamburgerDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlite(connectionString);

        return new GoodHamburgerDbContext(builder.Options);
    }
}
