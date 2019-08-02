using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Laklp.Platform.Data.Context
{
    internal class LaklpDbContextFactory : IDesignTimeDbContextFactory<LaklpDbContext>
    {
        public LaklpDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.development.json")
                .Build();

            var connectionString = configuration["Data:ConnectionString"];
            var optionsBuilder = new DbContextOptionsBuilder<LaklpDbContext>()
                .UseSqlServer(connectionString);

            return new LaklpDbContext(optionsBuilder.Options);
        }
    }
}