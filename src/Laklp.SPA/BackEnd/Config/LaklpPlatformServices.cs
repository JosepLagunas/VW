using AutoMapper;
using Boxed.AspNetCore;
using Laklp.Models.Settings;
using Laklp.Platform.Data.Repositories.Interfaces;
using Laklp.Services.Resources;
using Laklp.Services.S3Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laklp.Config
{
    public static class LaklpPlatformServices
    {
        public static IServiceCollection AddLaklpPlatformServices(this IServiceCollection 
        services, IConfiguration configuration)
        {
            services.AddScoped<IS3Service, S3Service>(opt =>
            {
                var awsAccessCredentials =
                    configuration.GetSection<AWSAccessCredentials>(
                        "Authentication:AWS:AWSCredentials");
                var s3Settings = configuration.GetSection<S3Settings>("S3:Settings");

                return new S3Service(awsAccessCredentials, s3Settings);
            });

            services.AddScoped<IResourcesService, ResourcesService>(opt =>
            {
                var provider = services.BuildServiceProvider();

                var documentarySettings =
                    configuration
                        .GetSection<DocumentarySettings>("DocumentaryResourcesSettings");

                var s3Service = provider.GetService<IS3Service>();
                var documentaryRepository = provider.GetService<IDocumentaryRepository>();
                var mapper = provider.GetService<IMapper>();

                return new ResourcesService(documentaryRepository, s3Service, documentarySettings,
                    mapper);
            });

            return services;
        }
    }
}