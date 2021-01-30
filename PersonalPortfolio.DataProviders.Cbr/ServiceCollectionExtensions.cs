using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.DataProviders.Cbr
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCbrDataProviders(this IServiceCollection services, string cbrEndpoint)
        {
            return services
                .AddTransient<IRatesProvider, RatesProvider>()
                .AddTransient<ICbrClient, CbrClientClient>(
                    _ => new CbrClientClient(CbrClientClient.DefaultBindingForEndpoint(), cbrEndpoint));
        }
    }
}
