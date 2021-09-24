using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnFramework
{
    public class ExecutionContext<TInput> : IExecutionContext<TInput>
    {
        public CompilerType CompilerType { get; set; }

        public string AdditionalCompilerArguments { get; set; }

        public string Code { get; set; }

        public byte[] FileContent { get; set; }

        public string AllowedFileExtensions { get; set; }

        public int TimeLimit { get; set; }

        public int MemoryLimit { get; set; }

        public TInput Input { get; set; }
    }
}
