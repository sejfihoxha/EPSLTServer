using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EPSLTTaskServer.Infrastructure.EntityFramework
{
    public class DiscountDbContextFactory : IDesignTimeDbContextFactory<DiscountDbContext>
    {
        public DiscountDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DiscountDb");

            var optionsBuilder = new DbContextOptionsBuilder<DiscountDbContext>()
                .UseSqlServer(connectionString);

            return new DiscountDbContext(optionsBuilder.Options);
        }
    }

}
