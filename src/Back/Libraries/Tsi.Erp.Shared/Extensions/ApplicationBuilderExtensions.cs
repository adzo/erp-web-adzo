
using Microsoft.AspNetCore.Builder;

namespace Tsi.Erp.Shared.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CreateMicroservicePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
