using Nest.Application.Repositories;
using Nest.Domain.Entities;
using Nest.Infrastructure.Persistance;

namespace Nest.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
