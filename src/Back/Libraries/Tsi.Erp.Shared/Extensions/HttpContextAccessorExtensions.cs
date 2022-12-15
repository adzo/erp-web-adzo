using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tsi.Erp.Shared.Extensions
{
    internal static class HttpContextAccessorExtensions
    {
        internal static TDbContext GetApplicationDbContext<TDbContext>(this IHttpContextAccessor contextAccessor) where TDbContext : DbContext
        {
            if (contextAccessor == null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: Context accessor is null!");
            }

            if (typeof(TDbContext) is null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: provided type is is null!");
            }

            if (contextAccessor.HttpContext is null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: HttpContext in Context accessor is null!");
            }

            var dbContext = contextAccessor.HttpContext
                .RequestServices
                .GetService(typeof(TDbContext));

            if (dbContext is null)
            {
                throw new ApplicationException($"HttpContextAccessorExtensions.GetApplicationContext: DbContext of type {typeof(TDbContext).Name} not found in DI!");
            }

            return (TDbContext)dbContext;
        }

        internal static IEnumerable<TService> GetRequiredServices<TService>(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: Context accessor is null!");
            }

            if (contextAccessor.HttpContext is null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: HttpContext in Context accessor is null!");
            }

            return contextAccessor.HttpContext
                .RequestServices
                .GetServices<TService>();
        }
    }
}
