using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Domain.Entities;
using Nest.Infrastructure.Persistance.Configurations.Common;

namespace Nest.Infrastructure.Persistance.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(550).IsRequired(false);
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.DiscountPrice).IsRequired();
        base.Configure(builder);
    }
}
