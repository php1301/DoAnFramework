using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework.src
{
    public class CreateSubmissionExecutor: ISubmissionWorker
    {

        private readonly int portNumber;

        public CreateSubmissionExecutor(int portNumber)
            => this.portNumber = portNumber;

        public string Location
            => this.portNumber.ToString();

        public IExecutionResult<TResult> RunSubmission<TInput, TResult>(Submission<TInput> submission)
            where TResult : class, ISingleCodeRunResult, new()
            => new SubmissionExecutor(this.portNumber)
                .Execute<TInput, TResult>(submission);
    }
}
