
    using System.ComponentModel.DataAnnotations;
namespace DoAnFramework.src.Models
{
    public class SubmissionModel
    {
        public int ProblemId { get; set; }

        public int SubmissionTypeId { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 5)]
        [Required]
        public string Content { get; set; }
    }
}