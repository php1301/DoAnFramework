using System.Data.Entity.ModelConfiguration;
using DoAnFramework.src.Models;

namespace DoAnFramework.src.Configurations
{
 

    public class ParticipantScoresConfiguration : EntityTypeConfiguration<ParticipantScore>
    {
        public ParticipantScoresConfiguration()
        {
            /*
             * The following configuration fixes:
             * Introducing FOREIGN KEY constraint 'FK_dbo.ParticipantScores_dbo.Problems_ProblemId' on table 'ParticipantScores' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
             */
            this.HasRequired(x => x.Problem)
                .WithMany(x => x.ParticipantScores)
                .HasForeignKey(x => x.ProblemId)
                .WillCascadeOnDelete(false);
        }
    }
}
