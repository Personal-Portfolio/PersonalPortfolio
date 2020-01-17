using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class SecurityTypeConfiguration: EntityConfiguration<SecurityType>
    {
        public override void Configure(EntityTypeBuilder<SecurityType> builder)
        {
            builder.ToTable("SecurityTypes");

            base.Configure(builder);
        }
    }
}
