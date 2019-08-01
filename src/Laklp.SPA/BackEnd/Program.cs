using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Laklp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureLogging((context, logging) =>
                {
                    // Cleaning the providers because WebHost.CreateDefaultBuilder
                    // add logging.AddConsole() and logging.AddDebug() for
                    // staging and production environment
                    logging.ClearProviders();

                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                });
    }
}