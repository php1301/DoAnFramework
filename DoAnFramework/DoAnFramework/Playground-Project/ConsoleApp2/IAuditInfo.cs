using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
