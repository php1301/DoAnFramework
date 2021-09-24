using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public class SubmissionExecutor : ISubmissionExecutor
    {
        private readonly int portNumber;

        public SubmissionExecutor(int portNumber) => this.portNumber = portNumber;

        public IExecutionResult<TResult> Execute<TInput, TResult>(
            ISubmission submission)
            where TResult : ISingleCodeRunResult, new()
        {
            var executionStrategy = this.CreateExecutionStrategy(submission);

            var executionContext = this.CreateExecutionContext<TInput>(submission);

            return this.ExecuteSubmission<TInput, TResult>(executionStrategy, executionContext, submission);
        }

        private IExecutionStrategy CreateExecutionStrategy(ISubmission submission)
        {
            try
            {
                return SubmissionHelper.CreateExecutionStrategy();
            }
            catch (Exception ex)
            {
                submission.ProcessingComment = $"Exception in creating execution strategy: {ex.Message}";

                throw new Exception($"Exception in {nameof(this.CreateExecutionStrategy)}", ex);
            }
        }

        private IExecutionContext<TInput> CreateExecutionContext<TInput>(ISubmission submission)
        {
            try
            {
                return SubmissionHelper.CreateExecutionContext(
                    submission as Submission<TInput>);
            }
            catch (Exception ex)
            {
                submission.ProcessingComment = $"Exception in creating execution context: {ex.Message}";

                throw new Exception($"Exception in {nameof(this.CreateExecutionContext)}", ex);
            }
        }

        private IExecutionResult<TResult> ExecuteSubmission<TInput, TResult>(
            IExecutionStrategy executionStrategy,
            IExecutionContext<TInput> executionContext,
            ISubmission submission)
            where TResult : ISingleCodeRunResult, new()
        {
            try
            {
                return executionStrategy.SafeExecute<TInput, TResult>(executionContext);
            }
            catch (Exception ex)
            {
                submission.ProcessingComment = $"Exception in executing the submission: {ex.Message}";

                throw new Exception($"Exception in {nameof(this.ExecuteSubmission)}", ex);
            }
        }
    }
}
