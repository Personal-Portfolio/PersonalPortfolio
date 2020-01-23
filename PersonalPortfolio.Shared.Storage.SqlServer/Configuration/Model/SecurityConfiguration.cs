using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
{
    internal class SecurityConfiguration : EntityConfiguration<Security>
    {
        public override void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Securities");
            builder.HasAlternateKey(p => p.Ticker);
            
            builder.Property(p => p.Ticker)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(300)
                .HasDefaultValue("");

            builder.HasOne(security => security.Type)
                .WithMany()
                .HasForeignKey(security => security.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(security => security.BaseCurrency)
                .WithMany()
                .HasForeignKey(security => security.BaseCurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
