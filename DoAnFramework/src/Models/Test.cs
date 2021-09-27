namespace DoAnFramework.src.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Test
    {
        public byte[] Compress(string stringToCompress)
        {
            if (stringToCompress == null)
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(stringToCompress);

            using (var memoryStreamInput = new MemoryStream(bytes))
            {
                using (var memoryStreamOutput = new MemoryStream())
                {
                    using (var deflateStream = new DeflateStream(memoryStreamOutput, CompressionMode.Compress))
                    {
                        memoryStreamInput.CopyTo(deflateStream);
                    }

                    return memoryStreamOutput.ToArray();
                }
            }
        }
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
        [Key]
        public int Id { get; set; }

        public int ProblemId { get; set; }

        public virtual Problem Problem { get; set; }

        /// <remarks>
        /// Using byte[] (compressed with zip) to save database space.
        /// </remarks>
        public byte[] InputData { get; set; }

        [NotMapped]
        public string InputDataAsString
        {
            get => Decompress(this.InputData);

            set => this.InputData = Compress(value);
        }

        /// <remarks>
        /// Using byte[] (compressed with zip) to save database space.
        /// </remarks>
        public byte[] OutputData { get; set; }

        [NotMapped]
        public string OutputDataAsString
        {
            get => Decompress(this.OutputData);

            set => this.OutputData = Compress(value);
        }

        public bool IsTrialTest { get; set; }

        public bool IsOpenTest { get; set; }

        public bool HideInput { get; set; }

        public int OrderBy { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; } = new HashSet<TestRun>();
    }
}
