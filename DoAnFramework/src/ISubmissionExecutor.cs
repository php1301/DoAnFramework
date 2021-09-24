using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public interface ISubmissionExecutor
    {
        IExecutionResult<TResult> Execute<TInput, TResult>(ISubmission submission)
            where TResult : ISingleCodeRunResult, new();
    }
}
