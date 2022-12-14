using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tsi.Erp.Shared.Abstractions;

namespace Tsi.Erp.Shared.Internals
{
    internal class DatabaseTransaction<TDbContext>: IDatabaseTransaction where TDbContext: DbContext
    {
        private readonly TDbContext _context;

        public DatabaseTransaction(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.GetApplicationDbContext<TDbContext>(); 
        }

        public async Task BeginAsync()
        {  
            if(_context.Database.CurrentTransaction is not null)
            {
                var currentTransation = _context.Database.CurrentTransaction;
                throw new ApplicationException($"There is a currently opened transaction with database {_context.Database.ProviderName} | {currentTransation.TransactionId}");
            }

            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_context.Database.CurrentTransaction is null)
            { 
                throw new ApplicationException($"No transaction opened with database {_context.Database.ProviderName} to commit");
            }

            await _context.Database.CommitTransactionAsync();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
