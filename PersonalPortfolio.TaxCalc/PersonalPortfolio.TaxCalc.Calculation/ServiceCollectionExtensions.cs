using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Calculation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaxReportGenerator(this IServiceCollection services)
        {
            return services
                    .AddTransient<ITaxReportsProducer, TaxReportsProducer>()
                    .AddTransient<ITaxReportGenerator, TaxReportGenerator>();
        }
    }
}
