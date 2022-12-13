using Microsoft.Extensions.DependencyInjection;

namespace Tsi.Erp.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection BuildMicroserviceDependencies<TFlag>(this IServiceCollection services)
        {
            services.AddApplicationDependecies<TFlag>();

            services.AddControllers();

            return services;
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
