using Microsoft.EntityFrameworkCore;
using Nest.Domain.Entities;

namespace Nest.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
