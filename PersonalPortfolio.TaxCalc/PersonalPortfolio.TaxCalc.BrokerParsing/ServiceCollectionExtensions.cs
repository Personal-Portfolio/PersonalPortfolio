using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.BrokerParsing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInteractiveBrokersStockReportParser(this IServiceCollection services)
        {
            return services
                .AddTransient(_ => Encoding.Default.GetDecoder()) // TODO: Add some configuration
                .AddTransient<ITradeItemFactory, StockTradeItemFactory>()
                .AddTransient<IBrokerReportParser, BrokerReportParser>();
        }
    }
}
