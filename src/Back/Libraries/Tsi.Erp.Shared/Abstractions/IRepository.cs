using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.Shared.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Guid id, params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> Update(TEntity entity);
    }
}
