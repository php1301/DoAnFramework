using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{

    public class OutputResult : SingleCodeRunResult
    {
        public ProcessExecutionResultType ResultType { get; set; }

        public string Output { get; set; }
    }
}
