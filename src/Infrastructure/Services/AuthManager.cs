using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nest.Application.Dtos.Auth;
using Nest.Application.Services;
using Nest.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nest.Infrastructure.Services;

public class AuthManager : BaseManager, IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    public AuthManager(IMapper mapper, UserManager<AppUser> userManager,
                       RoleManager<IdentityRole> roleManager, IConfiguration configuration) : base(mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<(string result, bool success)> Login(LoginDto loginDto)
    {
        var exist = await _userManager.FindByNameAsync(loginDto.UserName);
        if (exist is null) return ("Invalid Credentials", false);

        var checkPass = await _userManager.CheckPasswordAsync(exist, loginDto.Password);
        if (!checkPass) return ("Invalid Credentials", false);

        List<Claim> claims = new List<Claim>();

        var existRoles = await _userManager.GetRolesAsync(exist);

        foreach (var role in existRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        claims.Add(new Claim(ClaimTypes.Name, loginDto.UserName));
        claims.Add(new Claim(ClaimTypes.Email, exist.Email!));
        claims.Add(new Claim("Fin", exist.Fin));


        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:secret"]!));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:issuer"],
            audience: _configuration["JwtSettings:audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:expires"]!)),
            signingCredentials: signingCredentials
            );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return (token, true);
    }

    public async Task<(string message, bool success)> Register(RegisterDto registerDto)
    {
        AppUser newUser = Mapper.Map<AppUser>(registerDto);
        var exist = await _userManager.FindByNameAsync(registerDto.UserName);
        if (exist != null) return ("User already exist", false);
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!_roleManager.Roles.Any()) await CreateRoles();
        await _userManager.AddToRoleAsync(newUser, "Member");

        if (!result.Succeeded) return (result.Errors.FirstOrDefault()!.Description, false);
        return ("Register Success", true);
    }

    private async Task CreateRoles()
    {
        await _roleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin" });
        await _roleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Member" });
    }
}

public abstract class BaseManager
{
    public IMapper Mapper { get; }
    public BaseManager(IMapper mapper)
    {
        Mapper = mapper;
    }
}