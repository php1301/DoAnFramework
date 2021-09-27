namespace DoAnFramework.src.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Tag : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public virtual ICollection<Problem> Problems { get; set; } = new HashSet<Problem>();
    }
}
