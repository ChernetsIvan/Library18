using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(a => a.AuthorId).IsRequired();
            Property(a => a.Name).IsRequired();
            Property(a => a.LastName).IsRequired();
        }
    }
}
