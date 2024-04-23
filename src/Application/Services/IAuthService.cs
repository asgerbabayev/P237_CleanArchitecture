using Nest.Application.Dtos.Auth;

namespace Nest.Application.Services;

public interface IAuthService
{
    Task<(string message, bool success)> Register(RegisterDto registerDto);
    Task<(string result, bool success)> Login(LoginDto loginDto);
}
