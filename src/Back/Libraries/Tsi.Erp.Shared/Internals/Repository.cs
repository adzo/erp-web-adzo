using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tsi.Erp.Shared.Abstractions;

namespace Tsi.Erp.Shared.Internals
{ 

    internal class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetAsync(CreateEqualityExpressionForId(id), cancellationToken);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(where, cancellationToken) ?? null;
        }

        public async Task<TEntity?> GetAsync(Guid id, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = _dbset.AsNoTracking().AsQueryable<TEntity>();

            if (propertySelectors is not null && propertySelectors.Any())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return await query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbset.AsNoTracking().Where(where).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entry = _dbset.Update(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(TEntity entity)
        {
            _dbset.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
