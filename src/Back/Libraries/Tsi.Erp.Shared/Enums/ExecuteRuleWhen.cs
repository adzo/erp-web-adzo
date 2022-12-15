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
        None = 0,
        /// <summary>
        /// TO be executed on all repository commands (before)
        /// </summary>
        BeforeAll = 1,
        /// <summary>
        /// TO be executed on all repository commands (after)
        /// </summary>
        AfterAll = 2,
        /// <summary>
        /// To be executed before the Insert command (happens before the save changes)
        /// </summary>
        BeforeInsert = 4,
        /// <summary>
        /// To be executed after the Insert command (happens before the save changes)
        /// </summary>
        AfterInsert = 8,
        /// <summary>
        /// To be executed before the Update command (happens before the save changes)
        /// </summary>
        BeforeUpdate = 16,
        /// <summary>
        /// To be executed after the Update command (happens before the save changes)
        /// </summary>
        AfterUpdate = 32,
        /// <summary>
        /// To be executed before the Delete command (happens before the save changes)
        /// </summary>
        BeforeDelete = 64,
        /// <summary>
        /// To be executed after the Delete command (happens before the save changes)
        /// </summary>
        AfterDelete = 128,
    }
}
