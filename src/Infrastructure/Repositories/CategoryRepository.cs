using Nest.Application.Repositories;
using Nest.Domain.Entities;
using Nest.Infrastructure.Persistance;

namespace Nest.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
