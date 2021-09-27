namespace DoAnFramework.src.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProblemGroup : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public int OrderBy { get; set; }

        public virtual ICollection<Problem> Problems { get; set; } = new HashSet<Problem>();
    }
}