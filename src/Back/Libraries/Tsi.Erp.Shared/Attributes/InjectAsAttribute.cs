using Tsi.Erp.Shared.Enums;

namespace Tsi.Erp.Shared
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAsAttribute<TInterface> : InjectAsAttribute
    {
        /// <summary>
        /// By default, Scoped is the selected lifetime
        /// </summary>
        /// <param name="lifetime"></param>
        public InjectAsAttribute(DependencyLifetime lifetime = DependencyLifetime.Scoped) : base(typeof(TInterface), lifetime)
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

    /// <summary>
    /// Tag required to mention when to execute the rule. If not mentioned, the rule will be executed always before all repository commands (Insert, Update, Delete)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RuleOnAttribute: Attribute
    {
        public ExecuteRuleWhen When { get; }

        /// <summary>
        /// By default, All will be selected
        /// <class>ExecuteRuleWhen</class>
        /// </summary>
        /// <param name="executionRules"></param>
        public RuleOnAttribute(ExecuteRuleWhen executionRules = ExecuteRuleWhen.All)
        {
            When = executionRules;
        }
    }
}
