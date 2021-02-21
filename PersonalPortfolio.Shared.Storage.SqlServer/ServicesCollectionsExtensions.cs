using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;
using PersonalPortfolio.Shared.Storage.Commands;
using PersonalPortfolio.Shared.Storage.Projections;
using PersonalPortfolio.Shared.Storage.Queries;
using PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model;

namespace PersonalPortfolio.Shared.Storage.SqlServer
{
    public static class ServicesCollectionsExtensions
    {
        public static IServiceCollection AddPortfolioSqlStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IContextModelConfigurator, ContextModelConfigurator>()
                .AddTransient<IBulkCommandsService, BulkCommandsService<PortfolioDbContext>>()
                .AddTransient<IMapper<Security, SecurityInfo>, SecurityInfoMapper>()
                .AddTransient<ISecurityQueryService, SecurityQueryService>()
                .AddTransient<IMapper<Currency, CurrencyInfo>, CurrencyInfoMapper>()
                .AddTransient<ICurrencyQueryService, CurrencyQueryService>()
                .AddTransient<ICurrencyCommandService, CurrencyCommandService>()
                .AddDbContext<PortfolioDbContext>(
                    options =>
                    {
                        var connectionString = configuration.GetConnectionString(Constants.ConnectionStringName)
                                               ?? throw new ArgumentNullException(nameof(configuration),
                                                   $"Connection string with name {Constants.ConnectionStringName} not found.");
                        options.UseSqlServer(connectionString,
                            o => o.MigrationsAssembly(typeof(Constants).Assembly.FullName));
                    }, ServiceLifetime.Transient);

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
