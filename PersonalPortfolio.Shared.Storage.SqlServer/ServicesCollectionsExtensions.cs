using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Storage.SqlServer.Configurations;

namespace PersonalPortfolio.Shared.Storage.SqlServer
{
    public static class ServicesCollectionsExtensions
    {
        public static IServiceCollection AddContextFactory(this IServiceCollection services)
        {
            services.AddTransient<IContextModelConfigurator, ContextModelConfigurator>();
            services.AddTransient<IContextFactory<PortfolioDbContext>, PortfolioDbContextFactory>();

            return services;
        }

        public static IServiceCollection AddDesignTimeDbContext(this IServiceCollection services)
        {
            services.AddTransient<IContextModelConfigurator, ContextModelConfigurator>();
            services.AddDbContext<PortfolioDbContext>(
                o => o.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=design_time;"));

            return services;
        }
    }
}
