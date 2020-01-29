using System;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.DataProviders.Moex
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddClient(this IServiceCollection services, Uri baseAddress)
        {
            return services
                .AddTransient<IExchangeClient, ExchangeClient>()
                .AddTransient<ISecurityInfoService, SecurityInfoService>()
                .AddHttpClient<IApiClient, ApiClient>(client =>
                    client.BaseAddress = baseAddress);
        }
    }
}
