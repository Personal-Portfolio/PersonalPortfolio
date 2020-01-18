using System;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage
{
    public class PortfolioDbContext: DbContext
    {
        private readonly IContextModelConfigurator _contextModelConfigurator;

        public PortfolioDbContext(DbContextOptions options, IContextModelConfigurator contextModelConfigurator)
            : base(options)
        {
            _contextModelConfigurator = contextModelConfigurator
                                        ?? throw new ArgumentNullException(nameof(contextModelConfigurator));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _contextModelConfigurator.Configure(builder);
        }

        public DbSet<Security> Securities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SecurityPrice> SecurityPrices { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}