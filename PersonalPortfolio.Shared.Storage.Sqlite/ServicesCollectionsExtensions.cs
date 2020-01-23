using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Sqlite
{
    public static class ServicesCollectionsExtensions
    {
        public static IServiceCollection AddContextFactory(this IServiceCollection services)
        {
            //services.AddTransient<IContextFactory<PortfolioDbContext>, PortfolioDbContextFactory>();

            return services;
        }

        public static IServiceCollection AddDesignTimeDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PortfolioDbContext>(
                o => o.UseSqlite("Data Source=design_time.db"));

            return services;
        }
    }
}
