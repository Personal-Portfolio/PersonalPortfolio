using System;
using System.Net.Http;
using Fluxor;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PersonalPortfolio.Shared.Abstractions;
using PersonalPortfolio.TaxCalc.BrokerParsing;
using PersonalPortfolio.TaxCalc.Calculation;
using PersonalPortfolio.TaxCalc.Client.Pages;
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

            services.AddMatBlazor();

            return services
                .AddScoped(_ => new HttpClient {BaseAddress = new Uri(environment.BaseAddress)})
                .AddInteractiveBrokersStockReportParser()
                .AddTaxReportGenerator()
                .AddRazorComponents()
                .AddTransient<IRatesProvider, RatesProvider>();
        }

        private static void AddProductionStateManagement(this IServiceCollection services)
        {
            services.AddFluxor(options => options
                    .ScanAssemblies(typeof(Program).Assembly)
                    .UseRouting());
        }

        private static void AddDevStateManagement(this IServiceCollection services)
        {
            services.AddFluxor(options =>
                    options
                        .ScanAssemblies(typeof(Program).Assembly)
                        .UseReduxDevTools()
                        .AddMiddleware<LoggingMiddleware>());
        }

        private static IServiceCollection AddRazorComponents(this IServiceCollection services)
        {
            return services
                .AddTransient<IndexPage>()
                .Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());
        }
    }
}
