using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    class BookAmountConfiguration : EntityTypeConfiguration<BookAmount>
    {
        public BookAmountConfiguration()
        {
            Property(ba => ba.BookAmountId).IsRequired();
            Property(ba => ba.BookId).IsRequired();
            Property(ba => ba.Amount).IsRequired();
        }
    }
}
