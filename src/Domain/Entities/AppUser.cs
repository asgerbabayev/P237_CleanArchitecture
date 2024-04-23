using Microsoft.AspNetCore.Identity;

namespace Nest.Domain.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public string Fin { get; set; } = null!;
    public string Address { get; set; } = null!;
}
