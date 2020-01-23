using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
{
    internal class ContextModelConfigurator: IContextModelConfigurator
    {
        public void Configure(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SecurityConfiguration());
            builder.ApplyConfiguration(new SecurityTypeConfiguration());
            builder.ApplyConfiguration(new SecurityPriceConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new CurrencyRateConfiguration());
        }
    }
}
