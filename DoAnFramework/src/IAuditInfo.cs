using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
