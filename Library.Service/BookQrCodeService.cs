using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Core.Utility;

namespace Library.Service
{
    public interface IBookQrCodeService
    {
        BookQrCode GetBookQrCode(string id);
        BookQrCode GetBookQrCodeByBookId(string bookId);
        string CreateBookQrCode(Book book);
        void UpdateBookQrCode(Book book);
        void DeleteBookQrCode(string id);
    }

    public class BookQrCodeService : IBookQrCodeService
    {
        private const int LenOfKeyId = 32;
        private readonly IBookQrCodeRepository _bookQrCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookQrCodeService(IBookQrCodeRepository bookQrCodeRepository, IUnitOfWork unitOfWork)
        {
            _bookQrCodeRepository = bookQrCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public BookQrCode GetBookQrCode(string id)
        {
            var bookQrCode = _bookQrCodeRepository.GetById(id);
            return bookQrCode;
        }

        public BookQrCode GetBookQrCodeByBookId(string bookId)
        {
            return _bookQrCodeRepository.Get(qr => qr.BookId == bookId);
        }        

        public string CreateBookQrCode(Book book)
        {
            BookQrCode bookQrCode = FormQr(book);
            _bookQrCodeRepository.Add(bookQrCode);
            SaveChanges();
            return bookQrCode.BookQrCodeId;
        }
        public void UpdateBookQrCode(Book book)
        {
            BookQrCode existQrCode = GetBookQrCodeByBookId(book.BookId);
            _bookQrCodeRepository.Delete(existQrCode);
            BookQrCode newQrCode = FormQr(book);
            _bookQrCodeRepository.Add(newQrCode);
            SaveChanges();
        }

        public void DeleteBookQrCode(string id)
        {
            var bookQrCode = _bookQrCodeRepository.GetById(id);
            _bookQrCodeRepository.Delete(bookQrCode);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        private BookQrCode FormQr(Book book)
        {
            string strToEncodeForQr = string.Format("Title {0}, Year {1}, ISBN {2}, http://[site.com/Books/bookId]", book.Title, book.Year, book.Isbn);
            var img = QRcodeGenerator.GenerateQrImage(strToEncodeForQr);
            BookQrCode bookQrCode = new BookQrCode
            {
                BookQrCodeId = UniqueStringKey.GetUniqueKey(LenOfKeyId),
                BookId = book.BookId,
                QrImageData = ImagePngConverter.ImageToByteArray(img),
                QrImageMimeType = "image/png"
            };
            return bookQrCode;
        }
    }
}
