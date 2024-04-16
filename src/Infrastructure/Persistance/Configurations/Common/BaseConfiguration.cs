using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Domain.Common;

namespace Nest.Infrastructure.Persistance.Configurations.Common;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Created).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.Modified).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.IP).IsRequired();
        builder.Property(x => x.Deleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => !x.Deleted);
    }
}
