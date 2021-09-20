using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public class TestResult: SingleCodeRunResult
    {
        public int Id { get; set; }

        public string Input { get; set; }

        public TestRunResultType ResultType { get; set; }

        public CheckerDetails CheckerDetails { get; set; }

        public bool IsTrialTest { get; set; }

        public string ExecutionComment { get; set; }
        public int TimeUsed { get; set; }
    }
}
