using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            Property(b => b.BookId).IsRequired();
            Property(b => b.Isbn).IsRequired();
            Property(b => b.Title).IsRequired();
            Property(b => b.Year).IsOptional();
            Property(b => b.Description).IsOptional();
            Property(b => b.PagesAmount).IsOptional();
            Property(b => b.PublishingHouse).IsOptional();
        }
    }
}
