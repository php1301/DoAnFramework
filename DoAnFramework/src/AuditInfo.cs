using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public abstract class AuditInfo : IAuditInfo
    {
       
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
       
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
