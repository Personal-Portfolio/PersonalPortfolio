using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
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

            builder.Property(e => e.Average).HasColumnType(Constants.Decimal);
            builder.Property(e => e.Open).HasColumnType(Constants.Decimal);
            builder.Property(e => e.Close).HasColumnType(Constants.Decimal);
            builder.Property(e => e.High).HasColumnType(Constants.Decimal);
            builder.Property(e => e.Low).HasColumnType(Constants.Decimal);

            base.Configure(builder);
        }
    }
}