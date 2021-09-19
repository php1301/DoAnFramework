using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace ConsoleApp2
{
    public class FilerHelpers
    {
        public static string BuildPath(params string[] paths) => Path.Combine(paths);

    }
}
