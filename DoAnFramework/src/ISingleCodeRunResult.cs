using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public interface ISingleCodeRunResult
    {
        int TimeUsed { get; }

        int MemoryUsed { get; }
    }
}
