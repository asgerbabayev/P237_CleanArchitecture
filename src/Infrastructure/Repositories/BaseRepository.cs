using Microsoft.EntityFrameworkCore;
using Nest.Application.Repositories;
using Nest.Domain.Common;
using Nest.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Nest.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        entity!.Deleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> Get(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var entity = _context.Set<TEntity>().AsQueryable();
        foreach (var type in includes)
        {
            entity = entity.Include(type);
        }
        return expression == null ?
               await entity.ToListAsync() :
               await entity.Where(expression).ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
