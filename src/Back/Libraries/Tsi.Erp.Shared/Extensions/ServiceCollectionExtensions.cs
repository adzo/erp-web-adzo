using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;
using Tsi.Erp.Shared.Abstractions;
using Tsi.Erp.Shared.Enums;
using Tsi.Erp.Shared.Internals;

namespace Tsi.Erp.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // As a Flag, use your context class!
        public static IServiceCollection BuildMicroserviceDependencies<TFlag>(this IServiceCollection services, IConfiguration configuration) where TFlag : DbContext
        {
            ControlConfiguration(configuration);

            services.AddApplicationDependencies<TFlag>();

            services.AddHttpContextAccessor();

            services.AddMicroServiceDbContext<TFlag>(configuration);

            services.AddRules<TFlag>();

            services.AddControllers();

            return services;
        }

        private static void AddRules<TFlag>(this IServiceCollection services)
        {
            typeof(TFlag)
                .Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any() && t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRule<>)).Any())
                .ToList()
                .ForEach(t =>
                {
                    InternalRegisterServiceAndBuildRules(t, services);
                });
        }

        private static void InternalRegisterServiceAndBuildRules(Type implementationRule, IServiceCollection services)
        {
            var iRuleInterface = implementationRule.GetInterfaces().FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IRule<>));

            if (iRuleInterface is null)
            {
                throw new ApplicationException($"Cannot load IRule interface for type {implementationRule.Name}");
            }

            services.AddScoped(iRuleInterface, implementationRule);

            var entityType = iRuleInterface.GetGenericArguments()[0];

            var ruleExecutionAttributes = implementationRule.GetCustomAttributes<RuleOnAttribute>();

            if (ruleExecutionAttributes is null)
            {
                throw new ArgumentException($"Type {implementationRule.Name} doesn't have the required {nameof(RuleOnAttribute)} ");
            }

            if (ruleExecutionAttributes.Count() > 1)
            {
                throw new ArgumentException($"Type {implementationRule.Name} has multiple {nameof(RuleOnAttribute)}. Only one attribute is allowed");
            }


            // we need to cache all rules
            if (!RulesCache.Rules.TryGetValue(entityType, out var entityRulesDictionnary))
            {
                entityRulesDictionnary = new ConcurrentDictionary<ExecuteRuleWhen, HashSet<Type>>();
                RulesCache.Rules.TryAdd(entityType, entityRulesDictionnary);
            }

            var ruleFlags = ruleExecutionAttributes.First().When;

            // before insert
            if((ruleFlags & ExecuteRuleWhen.BeforeAll) == ExecuteRuleWhen.BeforeAll || (ruleFlags & ExecuteRuleWhen.BeforeInsert) == ExecuteRuleWhen.BeforeInsert)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.BeforeInsert, implementationRule);
            }

            //before update
            if ((ruleFlags & ExecuteRuleWhen.BeforeAll) == ExecuteRuleWhen.BeforeAll || (ruleFlags & ExecuteRuleWhen.BeforeUpdate) == ExecuteRuleWhen.BeforeUpdate)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.BeforeUpdate, implementationRule);
            }

            //before delete
            if ((ruleFlags & ExecuteRuleWhen.BeforeAll) == ExecuteRuleWhen.BeforeAll || (ruleFlags & ExecuteRuleWhen.BeforeDelete) == ExecuteRuleWhen.BeforeDelete)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.BeforeInsert, implementationRule);
            }

            //after insert
            if ((ruleFlags & ExecuteRuleWhen.AfterAll) == ExecuteRuleWhen.AfterAll || (ruleFlags & ExecuteRuleWhen.AfterInsert) == ExecuteRuleWhen.AfterInsert)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.AfterInsert, implementationRule);
            }

            //after update
            if ((ruleFlags & ExecuteRuleWhen.AfterAll) == ExecuteRuleWhen.AfterAll || (ruleFlags & ExecuteRuleWhen.AfterUpdate) == ExecuteRuleWhen.AfterUpdate)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.AfterUpdate, implementationRule);
            }

            //after delete
            if ((ruleFlags & ExecuteRuleWhen.AfterAll) == ExecuteRuleWhen.AfterAll || (ruleFlags & ExecuteRuleWhen.AfterDelete) == ExecuteRuleWhen.AfterDelete)
            {
                AddRuleImplementationToDictionnary(entityRulesDictionnary, ExecuteRuleWhen.AfterDelete, implementationRule);
            }
        }

        private static void AddRuleImplementationToDictionnary(ConcurrentDictionary<ExecuteRuleWhen, HashSet<Type>> entityRulesDictionnary, ExecuteRuleWhen whenToExecute, Type implementationRule)
        {
            entityRulesDictionnary.AddOrUpdate(whenToExecute, new HashSet<Type>() { implementationRule }, (key, hashset) =>
            {
                hashset.Add(implementationRule);
                return hashset;
            });
        }

        private static void ControlConfiguration(IConfiguration configuration)
        {
            //Control if connection string set properly
            if (string.IsNullOrEmpty(configuration.GetConnectionString("default")))
            {
                throw new ApplicationException($"No connection string found in configuration with the name 'default'");
            }

        }

        private static IServiceCollection AddApplicationDependencies<TFlag>(this IServiceCollection services)
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
            if (!dbContextTypes.Any())
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
            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
