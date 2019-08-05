using Laklp.Platform.Data.Context;
using Laklp.Platform.Data.Repositories.Implementations;
using Laklp.Platform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Laklp.Platform.Data
{
    public static class Config
    {
        public static IServiceCollection AddLaklpPlatformDataAccess(
            this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<LaklpDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            serviceCollection.AddScoped<IDocumentaryRepository, DocumentaryRepository>();
            return serviceCollection;
        }
    }
}