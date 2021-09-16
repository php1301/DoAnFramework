using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Checker : DeletableEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string DllFile { get; set; }

        public string ClassName { get; set; }

        public string Parameter { get; set; }
    }
}
