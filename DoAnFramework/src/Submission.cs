using System.IO;
using System.IO.Compression;
using System.Text;

namespace DoAnFramework
{
    public class Submission<TInput> : ISubmission
    {
        public string Decompress(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            using (var memoryStreamInput = new MemoryStream(bytes))
            {
                using (var memoryStreamOutput = new MemoryStream())
                {
                    using (var deflateStream = new DeflateStream(memoryStreamInput, CompressionMode.Decompress))
                    {
                        deflateStream.CopyTo(memoryStreamOutput);
                    }

                    return Encoding.UTF8.GetString(memoryStreamOutput.ToArray());
                }
            }
        }
        private string code;

        public object Id { get; set; }

        public string AdditionalCompilerArguments { get; set; }

        public string ProcessingComment { get; set; }

        public int MemoryLimit { get; set; }

        public int TimeLimit { get; set; }

        public string Code
        {
            get => this.code
                ?? (string.IsNullOrWhiteSpace(this.AllowedFileExtensions)
                    ? Decompress(this.FileContent)
                    : null);
            set => this.code = value;
        }

        public byte[] FileContent { get; set; }

        public CompilerType CompilerType { get; set; }

        public ExecutionType ExecutionType { get; set; }

        public ExecutionStrategyType ExecutionStrategyType { get; set; }

        public string AllowedFileExtensions { get; set; }

        public TInput Input { get; set; }

        public int MaxPoints { get; set; }
    }
}
