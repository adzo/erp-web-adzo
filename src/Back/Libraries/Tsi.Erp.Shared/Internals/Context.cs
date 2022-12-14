using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.Shared.Internals
{
    internal class Context
    {
        internal static Type? ApplicationContextType { get; set; }
    }

    internal static class HttpContextAccessorExtensions
    {
        internal static TDbContext GetApplicationDbContext<TDbContext>(this IHttpContextAccessor contextAccessor) where TDbContext : DbContext
        {
            if (contextAccessor == null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: Context accessor is null!");
            }

            if(typeof(TDbContext) is null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: provided type is is null!");
            }

            if(contextAccessor.HttpContext is null)
            {
                throw new ApplicationException("HttpContextAccessorExtensions.GetApplicationContext: HttpContext in Context accessor is null!");
            }

            var dbContext = contextAccessor.HttpContext
                .RequestServices
                .GetService(typeof(TDbContext));

            if(dbContext is null)
            {
                throw new ApplicationException($"HttpContextAccessorExtensions.GetApplicationContext: DbContext of type {typeof(TDbContext).Name} not found in DI!");
            }

            return (TDbContext) dbContext;
        }
    }
}
