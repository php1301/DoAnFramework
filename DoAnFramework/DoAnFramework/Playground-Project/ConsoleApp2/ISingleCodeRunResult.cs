using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public interface ISingleCodeRunResult
    {
        int TimeUsed { get; }

        int MemoryUsed { get; }
    }
}
