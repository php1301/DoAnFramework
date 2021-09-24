using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public class ExecutionResult<TResult> : IExecutionResult<TResult>
       where TResult : ISingleCodeRunResult, new()
    {
        public bool IsCompiledSuccessfully { get; set; }

        public string CompilerComment { get; set; }

        public ICollection<TResult> Results { get; set; } = new List<TResult>();
    }
}
