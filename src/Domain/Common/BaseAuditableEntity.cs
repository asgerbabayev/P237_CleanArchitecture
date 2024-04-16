namespace Nest.Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public string CreatedBy { get; set; } = null!;
    public string IP { get; set; } = null!;
    public DateTime Created { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? Modified { get; set; }
}
