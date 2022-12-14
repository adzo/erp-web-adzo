namespace Tsi.Erp.Shared.Abstractions
{
    public interface IDatabaseTransaction
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
