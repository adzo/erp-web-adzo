using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.Shared.Enums;

namespace Tsi.Erp.Shared.Internals
{
    internal static class RulesCache
    {
        internal static ConcurrentDictionary<Type, ConcurrentDictionary<ExecuteRuleWhen, HashSet<Type>>> Rules = new();
    }
}
