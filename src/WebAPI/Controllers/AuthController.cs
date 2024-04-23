using Microsoft.AspNetCore.Mvc;
using Nest.Application.Dtos.Auth;
using Nest.Application.Services;

namespace Nest.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);
        return result.success ? Ok(new { success = result.success, message = result.message }) :
                                BadRequest(new { success = result.success, message = result.message });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);
        return result.success ? Ok(new { success = result.success, token = result.result }) :
                                BadRequest(new { success = result.success, message = result.result });
    }
}
