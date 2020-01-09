using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class SymbolRateConfiguration : EntityConfiguration<SymbolRate>
    {
        public override void Configure(EntityTypeBuilder<SymbolRate> builder)
        {
            builder.ToTable("Rates");
            builder.HasOne(m => m.SourceSymbol).WithMany()
                .HasForeignKey(k => k.SourceSymbolId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(m => m.TargetSymbol).WithMany()
                .HasForeignKey(k => k.TargetSymbolId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}