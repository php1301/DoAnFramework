using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public class SubmissionHelper
    {
        public static IExecutionStrategy CreateExecutionStrategy()
        {
            IExecutionStrategy executionStrategy;
            executionStrategy = new MySqlPrepareDatabaseAndRunQueriesExecutionStrategy(
                        "123",
                        "69",
                        "69");
            return executionStrategy;

        }

        public static IExecutionContext<TInput> CreateExecutionContext<TInput>(
          Submission<TInput> submission)
        {
            if (submission == null)
            {
                throw new ArgumentNullException(nameof(submission));
            }

            return new ExecutionContext<TInput>
            {
                AdditionalCompilerArguments = submission.AdditionalCompilerArguments,
                Code = submission.Code,
                FileContent = submission.FileContent,
                AllowedFileExtensions = submission.AllowedFileExtensions,
                CompilerType = submission.CompilerType,
                MemoryLimit = submission.MemoryLimit,
                TimeLimit = submission.TimeLimit,
                Input = submission.Input
            };
        }
    }
}
