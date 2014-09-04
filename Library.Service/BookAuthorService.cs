using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Domain.Utility;
using Library.Web.Utility;

namespace Library.Service
{
    public interface IBookAuthorService
    {
        IEnumerable<BookAuthor> GetBookAuthors();
        IEnumerable<BookAuthor> GetBookAuthors(int startIndex, int count);
        /*IEnumerable<BookAuthor> GetBookAuthors(int startIndex, int count, string filtering);*/
        BookAuthor GetBookAuthor(string id);        
        string CreateBookAuthor(BookAuthor bookAuthor);
        void UpdateBookAuthor(BookAuthor bookAuthor);
        void DeleteBookAuthor(string id);
        IEnumerable<Book> GetBooksByAuthorId(string authorId);
        IEnumerable<Author> GetAuthorsByBookId(string bookId);
        int GetBookAuthorsCount();
    }

    public class BookAuthorService : IBookAuthorService
    {
        private const int LenOfKeyId = 32;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public BookAuthorService(IBookAuthorRepository bookAuthorRepository, IUnitOfWork unitOfWork, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public IEnumerable<BookAuthor> GetBookAuthors()
        {
            return _bookAuthorRepository.GetAll();
        }

        public IEnumerable<BookAuthor> GetBookAuthors(int startIndex, int count)
        {
            return _bookAuthorRepository.GetAll().Skip(startIndex).Take(count);
        }

        /*public IEnumerable<BookAuthor> GetBookAuthors(int startIndex, int count, string filtering)
        {
            IEnumerable<Book> books = _bookRepository.GetAll();  //  !  !   !  пока хард-код)
            IEnumerable<Book> tempBooks = new List<Book>();
            IEnumerable<Book> resultBooks = new List<Book>();
            
            IEnumerable<Author> authors = _authorRepository.GetAll();
            IEnumerable<Author> tempAuthors = new List<Author>();
            IEnumerable<Author> resultAuthors = new List<Author>();

            IEnumerable<BookAuthor> resultBookAuthors = new List<BookAuthor>();

            if (!string.IsNullOrEmpty(filtering) && books != null && authors != null)
            {                                                                               //сперва по названию книжки
                tempBooks = books.Where(p => p.Title.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                (resultBooks as List<Book>).AddRange(tempBooks);
                if (resultBooks.Count() < count)
                {                                                                           //потом по фамилии и имени автора
                    tempAuthors = authors.Where(p => p.LastName.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                    (resultAuthors as List<Author>).AddRange(tempAuthors);
                    if (resultAuthors.Count() + resultBooks.Count() < count)
                    {
                        tempAuthors = authors.Where(p => p.Name.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                        (resultAuthors as List<Author>).AddRange(tempAuthors);
                        resultAuthors = resultAuthors.Distinct().ToList();
                    }
                }

                if (resultBooks.Any())
                {
                    var bookAuthors = OnBaseBooksSelectBookAuthors(resultBooks);
                    (resultBookAuthors as List<BookAuthor>).AddRange(bookAuthors);
                    resultBooks = new List<Book>();
                }
                if (resultAuthors.Any())
                {
                    var bookAuthors = OnBaseAuthorsSelectBookAuthors(resultAuthors);
                    (resultBookAuthors as List<BookAuthor>).AddRange(bookAuthors);
                    resultBookAuthors = resultBookAuthors.Distinct(new BookAuthorComparer()).ToList();
                }

                int yetNeed = count - resultBookAuthors.Count();
                if (yetNeed > 0)                                                           //потом по всем оставшимся полям книги
                {
                    tempBooks = books.Where(p => p.PublishingHouse != null && p.PublishingHouse.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                    (resultBooks as List<Book>).AddRange(tempBooks);
                    if (resultBooks.Count() < yetNeed)
                    {
                        tempBooks = books.Where(p => p.PagesAmount != 0 && p.PagesAmount.ToString().IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                        (resultBooks as List<Book>).AddRange(tempBooks);
                        resultBooks = resultBooks.Distinct().ToList();
                    }
                    if (resultBooks.Count() < yetNeed)
                    {
                        tempBooks = books.Where(p => p.Year != 0 && p.Year.ToString().IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                        (resultBooks as List<Book>).AddRange(tempBooks);
                        resultBooks = resultBooks.Distinct().ToList();
                    }
                    if (resultBooks.Count() < yetNeed)
                    {
                        tempBooks = books.Where(p => p.Isbn != null && p.Isbn.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                        (resultBooks as List<Book>).AddRange(tempBooks);
                        resultBooks = resultBooks.Distinct().ToList();
                    }
                    if (resultBooks.Count() < yetNeed)
                    {
                        tempBooks = books.Where(p => p.Description != null && p.Description.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                        (resultBooks as List<Book>).AddRange(tempBooks);
                        resultBooks = resultBooks.Distinct().ToList();
                    }
                }
                if (resultBooks.Any())
                {
                    var bookAuthors = OnBaseBooksSelectBookAuthors(resultBooks);
                    (resultBookAuthors as List<BookAuthor>).AddRange(bookAuthors);
                    resultBookAuthors = resultBookAuthors.Distinct(new BookAuthorComparer()).ToList();
                }
            }
            return resultBookAuthors.Skip(startIndex).Take(count);
        }*/

        private IEnumerable<BookAuthor> OnBaseBooksSelectBookAuthors(IEnumerable<Book> books)
        {
            var bookAuthors = from b in books
                             join ba in _bookAuthorRepository.GetAll() on b.BookId equals ba.BookId
                             select new BookAuthor
                             {
                                 BookAuthorId = ba.BookAuthorId,
                                 BookId = b.BookId,
                                 AuthorId = ba.AuthorId
                             };
            return bookAuthors;
        }

        private IEnumerable<BookAuthor> OnBaseAuthorsSelectBookAuthors(IEnumerable<Author> authors)
        {
            var bookAuthors = from a in authors
                             join ba in _bookAuthorRepository.GetAll() on a.AuthorId equals ba.AuthorId
                             select new BookAuthor
                             {
                                 BookAuthorId = ba.BookAuthorId,
                                 BookId = ba.BookId,
                                 AuthorId = a.AuthorId
                             };
            return bookAuthors;
        }

        public BookAuthor GetBookAuthor(string id)
        {
            var bookAuthor = _bookAuthorRepository.GetById(id);
            return bookAuthor;
        }

        public string CreateBookAuthor(BookAuthor bookAuthor)
        {
            bookAuthor.BookAuthorId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            _bookAuthorRepository.Add(bookAuthor);
            SaveChanges();
            return bookAuthor.BookAuthorId;
        }
        public void UpdateBookAuthor(BookAuthor bookAuthor)
        {
            _bookAuthorRepository.Update(bookAuthor);
            SaveChanges();
        }

        public void DeleteBookAuthor(string id)
        {
            var bookAuthor = _bookAuthorRepository.GetById(id);
            _bookAuthorRepository.Delete(bookAuthor);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Book> GetBooksByAuthorId(string authorId)
        {
            var books = from b in _bookRepository
                .GetAll()
                .OrderBy(b => b.Title)
                join ba in _bookAuthorRepository
                    .GetMany(ba => ba.AuthorId == authorId) on b.BookId equals ba.BookId
                select b;
            return books;
        }

        public IEnumerable<Author> GetAuthorsByBookId(string bookId)
        {
            var bookAuthors = _bookAuthorRepository.GetMany(ba => ba.BookId == bookId);
            var authors = from ba in bookAuthors
                          join a in _authorRepository.GetAll() on ba.AuthorId equals a.AuthorId
                          select a;
            return authors;
        }

        public int GetBookAuthorsCount()
        {
            return _bookAuthorRepository.GetAll().Count();
        }
    }
}
