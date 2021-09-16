using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class TestResult
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
