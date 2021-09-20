using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public class SqlResult
    {
        public bool Completed { get; set; }

        public ICollection<string> Results { get; set; } = new List<string>();
    }
}
