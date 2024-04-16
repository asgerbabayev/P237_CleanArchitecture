using Microsoft.EntityFrameworkCore;
using Nest.Application.Repositories;
using Nest.Domain.Entities;
using Nest.Infrastructure.Persistance;

namespace Nest.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        //_context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        category.Deleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        //_context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
