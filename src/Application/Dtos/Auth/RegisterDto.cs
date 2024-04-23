using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nest.Application.Dtos.Auth;
[ValidateNever]
public class RegisterDto
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Fin { get; set; } = null!;
    public string Password { get; set; } = null!;
}
