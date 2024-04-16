using Nest.Domain.Common;

namespace Nest.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    public string Image { get; set; } = null!;
}
