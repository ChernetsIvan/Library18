using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    public class UserBookConfiguration : EntityTypeConfiguration<UserBook>
    {
        public UserBookConfiguration()
        {
            Property(ub => ub.UserBookId).IsRequired();
            Property(ub => ub.UserProfileId).IsRequired();
            Property(ub => ub.BookId).IsRequired();
            Property(ub => ub.DateTaken).IsRequired();
            Property(ub => ub.DateReturned).IsOptional();
        }
    }
}
