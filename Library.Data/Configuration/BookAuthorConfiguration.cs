using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    public class BookAuthorConfiguration : EntityTypeConfiguration<BookAuthor>
    {
        public BookAuthorConfiguration()
        {
            Property(a => a.BookAuthorId).IsRequired();
            Property(a => a.AuthorId).IsRequired();
            Property(a => a.BookId).IsRequired();
        }
    }
}
