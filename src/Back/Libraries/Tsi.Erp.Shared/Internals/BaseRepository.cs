using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tsi.Erp.Shared.Internals
{

    internal class BaseRepository<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _dbset;

        protected DbContext _context;

        public BaseRepository(IHttpContextAccessor httpContextAccessor)
        {
            if(Context.ApplicationContextType is null)
            {
                throw new ApplicationException("BaseRepository.Ctor: Application context type not set!");
            }

            var context = httpContextAccessor.HttpContext?.RequestServices.GetService(Context.ApplicationContextType);

            if(context is DbContext dbContext)
            {
                _context= dbContext;
            }
            else
            {
                throw new ApplicationException("BaseRepository.Ctor: Could not load context from DI container");
            }

            _dbset = _context.Set<TEntity>();
        }

        internal Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(Guid id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Uid");

            var idValue = Convert.ChangeType(id, typeof(Guid));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
