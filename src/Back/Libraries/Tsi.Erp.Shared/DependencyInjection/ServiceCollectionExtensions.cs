using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tsi.Erp.Shared.Abstractions;
using Tsi.Erp.Shared.Internals;

namespace Tsi.Erp.Shared
{
    public static class ServiceCollectionExtensions
    {
        // As a Flag, use your context class!
        public static IServiceCollection BuildMicroserviceDependencies<TFlag>(this IServiceCollection services, IConfiguration configuration) where TFlag: DbContext
        {
            ControlConfiguration(configuration);

            services.AddApplicationDependecies<TFlag>();

            services.AddMicroServiceDbContext<TFlag>(configuration);

            services.AddControllers();

            return services;
        }

        private static void ControlConfiguration(IConfiguration configuration)
        {
            //Control if connection string set properly
            if (string.IsNullOrEmpty(configuration.GetConnectionString("default")))
            {
                throw new ApplicationException($"No connection string found in configuration with the name 'default'");
            }
            
        }

        private static IServiceCollection AddApplicationDependecies<TFlag>(this IServiceCollection services)
        {
            if (typeof(TFlag) == typeof(void))
            {
                throw new ArgumentException("Flag type cannot be of type void");
            }

            services.AddServices<TFlag>();

            return services;
        }

        private static IServiceCollection AddMicroServiceDbContext<TFlag>(this IServiceCollection services, IConfiguration configuration) where TFlag : DbContext
        {
            var dbContextTypes = typeof(TFlag)
                .Assembly
                .ExportedTypes
                .Where(t => t.IsAssignableTo(typeof(DbContext)) && !t.IsAbstract && !t.IsInterface)
                .ToList();

            // No dbContext found
            if(!dbContextTypes.Any() )
            {
                return services;
            }

            // check if multiple dbContext declared in micro service
            if (dbContextTypes.Count > 1)
            {
                throw new ApplicationException($"Cannot have multiple DbContext classes in your microservice {typeof(TFlag).Assembly}. Found types: {string.Join(',', dbContextTypes.Select(t => t.Name))}");
            }

            Context.ApplicationContextType = typeof(TFlag);

            services.AddDbContext<TFlag>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("default"));
            });

            services.AddScoped<IDatabaseTransaction, DatabaseTransaction<TFlag>>();

            return services;
        }

        

        private static IServiceCollection AddServices<TFlag>(this IServiceCollection services)
        {
            typeof(TFlag)
                .Assembly
                .ExportedTypes
                .Where(t => !t.IsAbstract && !t.IsInterface && t.GetCustomAttributes(typeof(InjectAsAttribute<>), true).Any())
                .ToList()
                .ForEach(concreteType =>
                {
                    var injectAttribute = (InjectAsAttribute)concreteType.GetCustomAttributes(typeof(InjectAsAttribute<>), true).First();
                    var interfaceType = injectAttribute.InterfaceType;

                    switch (injectAttribute.Lifetime)
                    {
                        case DependencyLifetime.Singleton:
                            services.AddSingleton(interfaceType, concreteType);
                            break;
                        case DependencyLifetime.Scoped:
                            services.AddScoped(interfaceType, concreteType);
                            break;
                        case DependencyLifetime.Transiant:
                            services.AddTransient(interfaceType, concreteType);
                            break;
                        default:
                            break;
                    }
                });

            return services;
        }
    }
}
