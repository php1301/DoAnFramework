using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
