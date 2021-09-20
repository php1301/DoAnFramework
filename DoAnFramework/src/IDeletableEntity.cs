using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
