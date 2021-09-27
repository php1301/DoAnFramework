using System.Data.Entity.ModelConfiguration;

using DoAnFramework.src.Models;
namespace DoAnFramework.src.Configurations
{
 

    public class ParticipantAnswersConfiguration : EntityTypeConfiguration<ParticipantAnswer>
    {
        public ParticipantAnswersConfiguration()
        {
            /*
             * The following configuration fixes:
             * Introducing FOREIGN KEY constraint 'FK_dbo.ParticipantAnswers_dbo.ContestQuestions_ContestQuestionId' on table 'ParticipantAnswers' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
             */
            this.HasRequired(x => x.ContestQuestion)
                .WithMany(x => x.ParticipantAnswers)
                .HasForeignKey(x => x.ContestQuestionId)
                .WillCascadeOnDelete(false);
        }
    }
}