using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class SecurityConfiguration : EntityConfiguration<Security>
    {
        public override void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Securities");
            builder.Property(p => p.Ticker).HasMaxLength(300).HasDefaultValue("");

            builder.HasOne(security => security.Type)
                .WithMany()
                .HasForeignKey(security => security.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
