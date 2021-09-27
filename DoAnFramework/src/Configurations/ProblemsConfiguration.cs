using System.Data.Entity.ModelConfiguration;

using DoAnFramework.src.Models;
namespace DoAnFramework.src.Configurations
{
    

    public class ProblemsConfiguration : EntityTypeConfiguration<Problem>
    {
        public ProblemsConfiguration()
        {
            /*
             * The following configuration fixes:
             * Introducing FOREIGN KEY constraint 'FK_dbo.Problems_dbo.ProblemGroups_ProblemGroupId' on table 'Problems' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
             */
            this.HasRequired(p => p.ProblemGroup)
                .WithMany(c => c.Problems)
                .HasForeignKey(p => p.ProblemGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}