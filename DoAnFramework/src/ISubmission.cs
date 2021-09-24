using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public interface ISubmission
    {
        object Id { get; }

        string AdditionalCompilerArguments { get; }

        string ProcessingComment { get; set; }

        int MemoryLimit { get; }

        int TimeLimit { get; }

        string Code { get; }

        byte[] FileContent { get; }

        string AllowedFileExtensions { get; }

        CompilerType CompilerType { get; }

        ExecutionType ExecutionType { get; }

        ExecutionStrategyType ExecutionStrategyType { get; }
    }
}
