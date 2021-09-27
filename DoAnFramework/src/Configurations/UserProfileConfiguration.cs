using System.Data.Entity.ModelConfiguration;

using DoAnFramework.src.Models;

namespace DoAnFramework.src.Configurations
{
   
    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            this.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
