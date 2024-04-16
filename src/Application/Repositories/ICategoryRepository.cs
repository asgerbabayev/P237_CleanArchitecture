using Nest.Domain.Entities;

namespace Nest.Application.Repositories;

public interface ICategoryRepository
{
    Task CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
    Task<Category> Get(int id);
    Task<List<Category>> GetAll();
}
