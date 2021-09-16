using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public interface IExecutionContext<TInput>
    {
        CompilerType CompilerType { get; }

        string AdditionalCompilerArguments { get; }

        string Code { get; set; }

        byte[] FileContent { get; set; }

        string AllowedFileExtensions { get; }

        int TimeLimit { get; }

        int MemoryLimit { get; }

        TInput Input { get; set; }

    }
}
