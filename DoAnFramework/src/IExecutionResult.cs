using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
        public interface IExecutionResult<TResult>
        where TResult : ISingleCodeRunResult, new()
        {
            bool IsCompiledSuccessfully { get; set; }

            string CompilerComment { get; set; }

            ICollection<TResult> Results { get; }
    }
}
