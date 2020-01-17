using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class SecurityPriceConfiguration : EntityConfiguration<SecurityPrice>
    {
        public override void Configure(EntityTypeBuilder<SecurityPrice> builder)
        {
            builder.ToTable("SecurityPrices");

            builder.HasAlternateKey(u => new { u.TradeDate, u.Security });
            builder.Property(p => p.TradeDate)
                .HasColumnType(Constants.Date)
                .IsRequired();

            builder.HasOne(rate => rate.Security)
                .WithMany(symbol => symbol.Prices)
                .HasForeignKey(security => security.SecurityId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}