using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configuration.Model
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(m => m.DateCreated).HasColumnType(Constants.DateTime2).HasDefaultValueSql(Constants.SysDateTime);
            builder.Property(p => p.DateUpdated).HasColumnType(Constants.DateTime2);
        }
    }
}