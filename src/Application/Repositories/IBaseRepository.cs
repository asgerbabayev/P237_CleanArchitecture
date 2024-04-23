using Nest.Domain.Common;
using System.Linq.Expressions;

namespace Nest.Application.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseAuditableEntity
{
    Task CreateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<TEntity?> Get(int id);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression);
    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task UpdateAsync(TEntity entity);
}
