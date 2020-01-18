using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class SecurityPriceConfiguration : CurrencyLinkedEntityConfiguration<SecurityPrice>
    {
        public override void Configure(EntityTypeBuilder<SecurityPrice> builder)
        {
            builder.ToTable("SecurityPrices");

            builder.HasAlternateKey(u => new { u.TradeDate, u.SecurityId });
            builder.Property(p => p.TradeDate)
                .HasColumnType(Constants.Date)
                .IsRequired();

            builder.HasOne(price => price.Security)
                .WithMany(security => security.Prices)
                .HasForeignKey(price => price.SecurityId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}