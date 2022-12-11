using Microsoft.Extensions.DependencyInjection;
using System.Reflection.PortableExecutable;
using Tsi.Erp.Extensions.Attributes;

namespace Tsi.Erp.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependecies<TFlag>(this IServiceCollection services)
        {
            if(typeof(TFlag) == typeof(void))
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
                .ForEach(t =>
                {
                    var injectAttribute = (InjectAsAttribute)t.GetCustomAttributes(typeof(InjectAsAttribute<>), true).First();
                    var interfaceType = injectAttribute.InterfaceType;

                    switch (injectAttribute.Lifetime)
                    {
                        case Enums.DependencyLifetime.Singleton:
                            services.AddSingleton(interfaceType, t);
                            break;
                        case Enums.DependencyLifetime.Scoped:
                            services.AddScoped(interfaceType, t);
                            break;
                        case Enums.DependencyLifetime.Transiant:
                            services.AddTransient(interfaceType, t);
                            break;
                        default:
                            break;
                    }
                });

            return services;
        }
    }
}
