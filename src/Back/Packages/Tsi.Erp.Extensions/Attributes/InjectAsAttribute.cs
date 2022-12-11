using Tsi.Erp.Extensions.Enums;

namespace Tsi.Erp.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAsAttribute<TInterface>: InjectAsAttribute
    { 
        /// <summary>
        /// By default, Scoped is the selected lifetime
        /// </summary>
        /// <param name="lifetime"></param>
        public InjectAsAttribute(DependencyLifetime lifetime = DependencyLifetime.Scoped): base(typeof(TInterface), lifetime)
        { 
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAsAttribute : Attribute
    {
        internal DependencyLifetime Lifetime { get; }
        internal Type InterfaceType { get; }
        /// <summary>
        /// By default, Scoped is the selected lifetime
        /// </summary>
        /// <param name="lifetime"></param>
        internal InjectAsAttribute(Type interfaceType, DependencyLifetime lifetime = DependencyLifetime.Scoped)
        {
            Lifetime = lifetime;
            InterfaceType = interfaceType;
        }
    }
}
