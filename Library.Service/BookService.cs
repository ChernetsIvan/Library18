using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Library.Core.Models;
using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Core.Utility;

namespace Library.Service
{
    public interface IBookService              
    {
        //IEnumerable<BookModel> GetBooks();
        //IEnumerable<BookModel> GetBooks(string sorting);
        //IEnumerable<BookModel> GetBooks(int startIndex, int count);
        IEnumerable<BookModel> GetBooks(int startIndex, int count, string sorting);
        Book GetBook(string id);
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

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
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

        public IEnumerable<BookModel> GetBooks(int startIndex, int count, string sorting)
        {
            var bookAmounts = from b in books
                              join ba in _bookAmountService.GetBookAmounts() on b.BookId equals ba.BookId
                              select new BookAmount
                              {
                                  BookAmountId = ba.BookAmountId,
                                  BookId = ba.BookId,
                                  Amount = ba.Amount
                              };
            var fullBookVms = from b in books
                              join ba in bookAmounts on b.BookId equals ba.BookId
                              select new FullBookViewModel
                              {
                                  BookId = b.BookId,
                                  Title = b.Title,
                                  Isbn = b.Isbn,
                                  Year = b.Year,
                                  Description = b.Description,
                                  PagesAmount = b.PagesAmount,
                                  PublishingHouse = b.PublishingHouse,
                                  BookAmount = ba.Amount,
                                  Authors = new List<AuthorViewModel>()
                              };
            foreach (var fullBookVm in fullBookVms)
            {
                fullBookVm.Authors.AddRange(_bookAuthorService.GetAuthorsByBookId(fullBookVm.BookId).Select(Mapper.Map<Author, AuthorViewModel>).ToList()); //не работает
            }

            IEnumerable<Book> books = _bookRepository.GetAll().Skip(startIndex).Take(count);
            Sorting(ref books, sorting);
            return books; 
        }

        public Book GetBook(string id)
        {
            var book = _bookRepository.GetById(id);
            return book;
        }

        public string CreateBook(BookModel book)
        {
            book.BookId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            _bookRepository.Add(book);
            SaveChanges();
            return book.BookId;
        }
        public void UpdateBook(BookModel book)
        {
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
