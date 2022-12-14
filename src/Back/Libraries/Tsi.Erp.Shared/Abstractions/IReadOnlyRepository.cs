using System.Linq.Expressions;

namespace Tsi.Erp.Shared.Abstractions
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    }
}
