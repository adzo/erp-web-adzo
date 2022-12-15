using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tsi.Erp.Shared.Abstractions;
using Tsi.Erp.Shared.Enums;
using Tsi.Erp.Shared.Exceptions;
using Tsi.Erp.Shared.Extensions;

namespace Tsi.Erp.Shared.Internals
{

    internal class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {  
            _httpContextAccessor = httpContextAccessor;
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
            await ValidateAsync(ExecuteRuleWhen.BeforeInsert, entity);
            var entry = await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            await ValidateAsync(ExecuteRuleWhen.AfterInsert, entity);
            return entry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            await ValidateAsync(ExecuteRuleWhen.BeforeUpdate, entity);
            var entry = _dbset.Update(entity);
            await _context.SaveChangesAsync();
            await ValidateAsync(ExecuteRuleWhen.AfterUpdate, entity);
            return entry.Entity;
        }

        public async Task Delete(TEntity entity)
        {
            await ValidateAsync(ExecuteRuleWhen.BeforeDelete, entity);
            _dbset.Remove(entity);
            await ValidateAsync(ExecuteRuleWhen.AfterDelete, entity);
            await _context.SaveChangesAsync();
        }

        #region Rules
        private async Task ValidateAsync(ExecuteRuleWhen whenRule, TEntity entity)
        {
            if (!RulesCache.Rules.ContainsKey(typeof(TEntity)))
            {
                return;
            }

            var Validators = RulesCache.Rules[typeof(TEntity)];

            if (!Validators.ContainsKey(whenRule))
            {
                return;
            }

            var rules = _httpContextAccessor.GetRequiredServices<IRule<TEntity>>()
                    .Where(x => Validators[whenRule].Contains(x.GetType()));

            foreach (IRule<TEntity> rule in rules.OrderBy(r => r.Order))
            {
                var result = await rule.ValidateAsync(entity);
                if (!result)
                {
                    throw new BusinessValidationException(rule.ErrorMessage);
                }
            } 
        } 
        #endregion
    }
}
