namespace DoAnFramework.src.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProblemResource : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int ProblemId { get; set; }

        public Problem Problem { get; set; }

        [Required]
        [MinLength(0)]
        [MaxLength(50)]
        public string Name { get; set; }

/*        public ProblemResourceType Type { get; set; }
*/
        public byte[] File { get; set; }

        [MaxLength(50)]
        public string FileExtension { get; set; }

        public string Link { get; set; }

        public int OrderBy { get; set; }
    }
}
