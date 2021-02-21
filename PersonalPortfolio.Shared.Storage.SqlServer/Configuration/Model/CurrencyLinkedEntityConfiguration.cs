using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
{
    internal abstract class CurrencyLinkedEntityConfiguration<TRate> : EntityConfiguration<TRate>
        where TRate : CurrencyLinkedEntity
    {
        public override void Configure(EntityTypeBuilder<TRate> builder)
        {
            builder.HasOne(rate => rate.Currency)
                .WithMany()
                .HasForeignKey(rate => rate.CurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}