using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public interface ISubmissionWorker
    {
        string Location { get; }

        IExecutionResult<TResult> RunSubmission<TInput, TResult>(Submission<TInput> submission)
            where TResult : class, ISingleCodeRunResult, new();
    }
}
