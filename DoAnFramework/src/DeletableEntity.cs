using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

       
        public DateTime? DeletedOn { get; set; }
    }
}
