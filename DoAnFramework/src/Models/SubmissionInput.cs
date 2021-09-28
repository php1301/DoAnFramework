using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DoAnFramework.src.Models
{
    public class SubmissionInput: DeletableEntity
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

        public int? ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }

        public int? ProblemId { get; set; }

        public virtual Problem Problem { get; set; }

        public int? SubmissionTypeId { get; set; }

        public virtual SubmissionType SubmissionType { get; set; }

        /// <remarks>
        /// Using byte[] (compressed with deflate) to save database space for text inputs. For other file types the actual file content is saved in the field.
        /// </remarks>
        public byte[] Content { get; set; }
        public string FileExtension { get; set; }

        [NotMapped]
        public bool IsBinaryFile => !string.IsNullOrWhiteSpace(this.FileExtension);
        public string IpAddress { get; set; }

        /*        [NotMapped]
                public bool IsBinaryFile => !string.IsNullOrWhiteSpace(this.FileExtension);*/

        [NotMapped]
        public string ContentAsString
        {
            get
            {
                if (this.IsBinaryFile)
                {
                    throw new InvalidOperationException("This is a binary file (not a text submission).");
                }

                return Decompress(this.Content);
            }

            set
            {
                if (this.IsBinaryFile)
                {
                    throw new InvalidOperationException("This is a binary file (not a text submission).");
                }

                this.Content = Compress(value);
            }
        }

        public bool IsCompiledSuccessfully { get; set; }

        public string CompilerComment { get; set; }

        [Index]
        public bool? IsPublic { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; } = new HashSet<TestRun>();

        /// <summary>
        /// Cache field for submission test runs representing each test run result as an integer equal to <see cref="TestRunResultType"/>.
        /// The first integer represent the number of trial tests associated with this submissions.
        /// This field optimized database queries.
        ///
        /// Example: 300011002 means:
        /// - Three trial tests runs with 0 result (Correct Answer)
        /// - Five normal test runs with:
        ///   - Two 1 results (Wrong Answer)
        ///   - Two 0 results (Correct Answer)
        ///   - One 2 result (Time Limit)
        /// </summary>
        public string TestRunsCache { get; set; }

        public bool Processed { get; set; }

        public string ProcessingComment { get; set; }

        /// <summary>
        /// Cache field for submissions points (to speed-up some of the database queries)
        /// </summary>
        public int Points { get; set; }

        [NotMapped]
        public int CorrectTestRunsCount
        {
            get
            {
                return this.TestRuns.Count(x => x.ResultType == TestRunResultType.CorrectAnswer);
            }
        }

        [NotMapped]
        public int CorrectTestRunsWithoutTrialTestsCount
        {
            get
            {
                return this.TestRuns.Count(x => x.ResultType == TestRunResultType.CorrectAnswer && !x.Test.IsTrialTest);
            }
        }

        [NotMapped]
        public int IncorrectTestRunsWithoutTrialTestsCount
        {
            get
            {
                return this.TestRuns.Count(x => x.ResultType != TestRunResultType.CorrectAnswer && !x.Test.IsTrialTest);
            }
        }
/*
        [NotMapped]
        public int TestsWithoutTrialTestsCount
        {
            get
            {
                return this.Problem.Tests.Count(x => !x.IsTrialTest);
            }
        }*/

        public void CacheTestRuns()
        {
            if (this.TestRuns.Any())
            {
                var result = new StringBuilder();
                var trialTests = 0;

                var orderedTestRuns = this.TestRuns
                    .OrderByDescending(tr => tr.Test.IsTrialTest)
                    .ThenBy(tr => tr.Id);

                foreach (var testRun in orderedTestRuns)
                {
                    if (testRun.Test.IsTrialTest)
                    {
                        trialTests++;
                    }

                    result.Append((int)testRun.ResultType);
                }

                this.TestRunsCache = $"{trialTests}{result.ToString()}";
            }
        }
    }
}
