using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.DbInitializer.Configuration;
using PersonalPortfolio.Shared.Storage.SqlServer;
using Serilog;

namespace PersonalPortfolio.DbInitializer
{
    internal class Program
    {
        public static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureLogging(loggingConfiguration => loggingConfiguration.ClearProviders())
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services
                        .Configure<Financial>(hostingContext.Configuration.GetSection("Financial"))
                        .AddPortfolioSqlStorageServices(hostingContext.Configuration)
                        .AddHostedService<DbInitializerHostedService>();
                });

            await builder.RunConsoleAsync();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
            => new HostBuilder()
                .ConfigureServices(services => services.AddDesignTimeDbContext());
    }
}
