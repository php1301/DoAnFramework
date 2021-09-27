using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnFramework.src.Models
{
    public class SourceCode : DeletableEntity
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

        public string AuthorId { get; set; }

        public virtual UserProfile Author { get; set; }

        public int? ProblemId { get; set; }

        public virtual Problem Problem { get; set; }

        public byte[] Content { get; set; }

        [NotMapped]
        public string ContentAsString
        {
            get => Decompress(this.Content);

            set => this.Content = Compress(value);
        }

        public bool IsPublic { get; set; }
    }
}
