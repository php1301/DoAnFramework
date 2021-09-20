using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
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
