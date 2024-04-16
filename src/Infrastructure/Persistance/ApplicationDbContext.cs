using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nest.Application.Common.Interfaces;
using Nest.Domain.Common;
using Nest.Domain.Entities;
using System.Reflection;

namespace Nest.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IHttpContextAccessor _accessor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                IHttpContextAccessor accessor) : base(options)
    {
        _accessor = accessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _accessor.HttpContext.User.Identity.Name ?? "newUser";
                    entry.Entity.Created = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _accessor.HttpContext.User.Identity!.Name!;
                    entry.Entity.Modified = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
