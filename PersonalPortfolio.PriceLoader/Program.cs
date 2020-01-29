using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.DataProviders.Moex;
using PersonalPortfolio.PricesLoader.Configuration;
using Polly;
using Polly.Extensions.Http;
using Serilog;

namespace PersonalPortfolio.PricesLoader
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
                    var moexClientConfigurationSection = hostingContext.Configuration.GetSection("MoexClientConfiguration");
                    var moexClientConfiguration = moexClientConfigurationSection.Get<MoexClientConfiguration>();

                    services
                        .AddHostedService<LoaderHostedService>()
                        .AddClient(moexClientConfiguration.IssAddress)
                        .AddPolicyHandler(RetryPolicy)
                        .SetHandlerLifetime(TimeSpan.FromMinutes(5));
                });

            await builder.RunConsoleAsync();
        }

        private static IAsyncPolicy<HttpResponseMessage> RetryPolicy =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
