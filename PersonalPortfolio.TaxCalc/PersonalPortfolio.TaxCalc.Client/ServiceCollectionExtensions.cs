using System;
using System.Net.Http;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Abstractions;
using PersonalPortfolio.TaxCalc.BrokerParsing;
using PersonalPortfolio.TaxCalc.Calculation;
using PersonalPortfolio.TaxCalc.Client.Services;
using PersonalPortfolio.TaxCalc.Client.Store;

// TODO: research tree-shaking and environments
// ReSharper disable UnusedMember.Global

namespace PersonalPortfolio.TaxCalc.Client
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {

#if DEBUG
            services.AddDevStateManagement();
#else
            services.AddProductionStateManagement();
#endif

            return services
                .AddScoped(_ => new HttpClient {BaseAddress = new Uri(environment.BaseAddress)})
                .AddInteractiveBrokersStockReportParser()
                .AddTaxReportGenerator()
                .AddTransient<IRatesProvider, RatesProvider>();
        }

        public static IServiceCollection AddProductionStateManagement(this IServiceCollection services)
        {
            return services
                .AddFluxor(options => options
                    .ScanAssemblies(typeof(Program).Assembly)
                    .UseRouting());
        }

        public static IServiceCollection AddDevStateManagement(this IServiceCollection services)
        {
            return services
                .AddFluxor(options =>
                    options
                        .ScanAssemblies(typeof(Program).Assembly)
                        .UseReduxDevTools()
                        .AddMiddleware<LoggingMiddleware>());
        }
    }
}
