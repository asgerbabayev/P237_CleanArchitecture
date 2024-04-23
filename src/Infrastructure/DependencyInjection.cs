using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nest.Application.Repositories;
using Nest.Application.Services;
using Nest.Domain.Entities;
using Nest.Infrastructure.Persistance;
using Nest.Infrastructure.Repositories;
using Nest.Infrastructure.Services;
using System.Text;

namespace Nest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("mssql")));
        services.AddIdentity<AppUser, IdentityRole>(x =>
        {
            x.Password.RequireDigit = false;
            x.Password.RequireNonAlphanumeric = false;
            x.Password.RequiredLength = 6;
            x.Password.RequireUppercase = false;
            x.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidIssuer = configuration["JwtSettings:issuer"],
                ValidAudience = configuration["JwtSettings:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:secret"]!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryService, CategoryManager>();



        return services;
    }
}
