using System.Data.Entity.ModelConfiguration;
using Library.Model.Models;

namespace Library.Data.Configuration
{
    public class BookQrCodeConfiguration : EntityTypeConfiguration<BookQrCode>
    {
        public BookQrCodeConfiguration()
        {
            Property(q => q.BookQrCodeId).IsRequired();
            Property(q => q.BookId).IsRequired();
            Property(q => q.QrImageData).IsRequired();
            Property(q => q.QrImageMimeType).IsRequired();
        }
    }
}
