using System.Threading;

namespace Tsi.Erp.Shared.Abstractions
{
    public interface IRule<TEntity>
    {
        public int Order { get; }
        public string ErrorMessage { get; }
        Task<bool> ValidateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
