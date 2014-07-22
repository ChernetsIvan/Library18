using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            Property(up => up.UserProfileId).IsRequired();
            Property(up => up.Floor).IsRequired();
            Property(up => up.Gender).IsRequired();
            Property(up => up.ImageData).IsOptional();
            Property(up => up.ImageMimeType).IsOptional();
            Property(up => up.Name).IsRequired();
            Property(up => up.LastName).IsRequired();
            Property(up => up.Mail).IsOptional();
            Property(up => up.Phone).IsOptional();
            Property(up => up.PlaceDescription).IsOptional();
            Property(up => up.Room).IsRequired();
            Property(up => up.Skype).IsRequired();
        }
    }
}
