using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
{
    internal class CurrencyConfiguration : EntityConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasAlternateKey(u => u.Code);

            builder.Property(p => p.Code)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(300)
                .HasDefaultValue("");

            base.Configure(builder);
        }
    }
}
