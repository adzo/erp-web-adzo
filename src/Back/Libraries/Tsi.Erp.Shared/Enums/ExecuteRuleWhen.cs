using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.Shared.Enums
{
    /// <summary>
    /// Used to determine when to execute a rule class 
    /// </summary>
    [Flags]
    public enum ExecuteRuleWhen
    {
        /// <summary>
        /// TO be executed on all repository commands (before)
        /// </summary>
        All = 1,
        /// <summary>
        /// To be executed before the Insert command (happens before the save changes)
        /// </summary>
        BeforeInsert = 2,
        /// <summary>
        /// To be executed after the Insert command (happens before the save changes)
        /// </summary>
        AfterInsert = 4,
        /// <summary>
        /// To be executed before the Update command (happens before the save changes)
        /// </summary>
        BeforeUpdate = 8,
        /// <summary>
        /// To be executed after the Update command (happens before the save changes)
        /// </summary>
        AfterUpdate = 16,
        /// <summary>
        /// To be executed before the Delete command (happens before the save changes)
        /// </summary>
        BeforeDelete = 32,
        /// <summary>
        /// To be executed after the Delete command (happens before the save changes)
        /// </summary>
        AfterDelete = 64,
    }
}
