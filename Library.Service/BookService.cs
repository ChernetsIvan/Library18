using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Library.Domain.Models;
using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Domain.Utility;

namespace Library.Service
{
    public interface IBookService              
    {
        //IEnumerable<BookModel> GetBooks();
        //IEnumerable<BookModel> GetBooks(string sorting);
        //IEnumerable<BookModel> GetBooks(int startIndex, int count);
        IEnumerable<BookModel> GetBooks(int startIndex, int count, string sorting);
        BookModel GetBook(string id);
        string CreateBook(BookModel book);
        void UpdateBook(BookModel book);
        void DeleteBook(string id);
        int GetBookCount();
        void Sorting(ref IEnumerable<BookModel> books, string sorting);
    }

    public class BookService : IBookService
    {
        private const int LenOfKeyId = 32;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookAmountService _bookAmountService;
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IBookQrCodeService _bookQrCodeService;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, IBookAmountService bookAmountService, IBookAuthorService bookAuthorService, IBookQrCodeService bookQrCodeService)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _bookAmountService = bookAmountService;
            _bookAuthorService = bookAuthorService;
            _bookQrCodeService = bookQrCodeService;
        }

        //public IEnumerable<BookModel> GetBooks()
        //{
        //    return _bookRepository.GetAll();
        //}

        //public IEnumerable<BookModel> GetBooks(string sorting)
        //{
        //    var books = _bookRepository.GetAll();
        //    Sorting(ref books, sorting);
        //    return books;
        //}

        //public IEnumerable<BookModel> GetBooks(int startIndex, int count)
        //{
        //    return _bookRepository.GetAll().Skip(startIndex).Take(count);
        //}

        public IEnumerable<BookModel> GetBooks(int startIndex, int count, string sorting) //temp
        {
            IEnumerable<Book> books = _bookRepository.GetAll().Skip(startIndex).Take(count).ToList();          
            IEnumerable<BookModel> bookModels = books.Select(Mapper.Map<Book, BookModel>).ToList();
            foreach (var bookModel in bookModels)
            {
                bookModel.BookAmount = _bookAmountService.GetBookAmount(bookModel.BookId).Amount;
                bookModel.Authors.AddRange(
                    _bookAuthorService.GetAuthorsByBookId(bookModel.BookId)
                        .Select(Mapper.Map<Author, AuthorModel>)
                        .ToList());
                bookModel.QrCode = _bookQrCodeService.GetBookQrCode(bookModel.BookId);
            }
            Sorting(ref bookModels, sorting);
            return bookModels;
        }

        public BookModel GetBook(string id)
        {
            var book = _bookRepository.GetById(id);
            var bookModel = Mapper.Map<Book, BookModel>(book);
            return bookModel;
        }

        public string CreateBook(BookModel bookModel)
        {
            var book = Mapper.Map<BookModel, Book>(bookModel);
            book.BookId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            _bookRepository.Add(book);
            SaveChanges();
            return book.BookId;
        }
        public void UpdateBook(BookModel bookModel)
        {
            var book = Mapper.Map<BookModel, Book>(bookModel);
            _bookRepository.Update(book);
            SaveChanges();
        }

        public void DeleteBook(string id)
        {
            var book = _bookRepository.GetById(id);
            _bookRepository.Delete(book);
            SaveChanges();
        }

        public int GetBookCount()
        {
            return _bookRepository.GetAll().Count();
        }
        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Sorting(ref IEnumerable<BookModel> books, string sorting)
        {
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Title ASC"))
            {
                books = books.OrderBy(p => p.Title);
            }
            else if (sorting.Equals("Title DESC"))
            {
                books = books.OrderByDescending(p => p.Title);
            }
            else if (sorting.Equals("Isbn ASC"))
            {
                books = books.OrderBy(p => p.Isbn);
            }
            else if (sorting.Equals("Isbn DESC"))
            {
                books = books.OrderByDescending(p => p.Isbn);
            }
            else if (sorting.Equals("Year ASC"))
            {
                books = books.OrderBy(p => p.Year);
            }
            else if (sorting.Equals("Year DESC"))
            {
                books = books.OrderByDescending(p => p.Year);
            }
        }
    }
}
