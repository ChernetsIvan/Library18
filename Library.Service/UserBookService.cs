//using System.Collections.Generic;
//using System.Linq;
//using Library.Data.Infrastructure;
//using Library.Model.Models;
//using Library.Data.Repository;
//using Library.Core.Utility;

//namespace Library.Service
//{
//    public interface IUserBookService
//    {
//        IEnumerable<UserBook> GetUserBooks();
//        IEnumerable<UserBook> GetUserBooks(string sorting);
//        IEnumerable<UserBook> GetUserBooks(int startIndex, int count);
//        IEnumerable<UserBook> GetUserBooks(int startIndex, int count, string sorting);
//        UserBook GetUserBook(string id);
//        string CreateUserBook(UserBook userBook);
//        void UpdateUserBook(UserBook userBook);
//        void DeleteUserBook(string id);
//        IEnumerable<Book> GetBooksByUserId(string userId);
//        UserProfile GetUserProfileByBookId(string bookId);
//        int GetUserBookCount();
//    }

//    public class UserBookService : IUserBookService
//    {
//        private const int LenOfKeyId = 32;
//        private readonly IUserBookRepository _userBookRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IUserProfileRepository _userProfileRepository;
//        private readonly IBookRepository _bookRepository;

//        public UserBookService(IUserBookRepository userBookRepository, IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository, IBookRepository bookRepository)
//        {
//            _userBookRepository = userBookRepository;
//            _unitOfWork = unitOfWork;
//            _userProfileRepository = userProfileRepository;
//            _bookRepository = bookRepository;
//        }

//        public IEnumerable<UserBook> GetUserBooks()
//        {
//            return _userBookRepository.GetAll();
//        }

//        public IEnumerable<UserBook> GetUserBooks(string sorting)
//        {
//            var userBooks = _userBookRepository.GetAll();
//            Sorting(ref userBooks, sorting);
//            return userBooks;
//        }

//        public IEnumerable<UserBook> GetUserBooks(int startIndex, int count)
//        {
//            return _userBookRepository.GetAll().Skip(startIndex).Take(count);
//        }

//        public IEnumerable<UserBook> GetUserBooks(int startIndex, int count, string sorting)
//        {
//            var userBooks = _userBookRepository.GetAll().Skip(startIndex).Take(count);
//            Sorting(ref userBooks, sorting);
//            return userBooks;
//        }

//        public UserBook GetUserBook(string id)
//        {
//            var userBook = _userBookRepository.GetById(id);
//            return userBook;
//        }

//        public string CreateUserBook(UserBook userBook)
//        {
//            userBook.UserBookId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
//            _userBookRepository.Add(userBook);
//            SaveChanges();
//            return userBook.UserBookId;
//        }

//        public void UpdateUserBook(UserBook userBook)
//        {
//            _userBookRepository.Update(userBook);
//            SaveChanges();
//        }

//        public void DeleteUserBook(string id)
//        {
//            var userBook = _userBookRepository.GetById(id);
//            _userBookRepository.Delete(userBook);
//            SaveChanges();
//        }

//        public IEnumerable<Book> GetBooksByUserId(string userId)
//        {
//            var books = from b in _bookRepository
//                .GetAll()
//                .OrderBy(b => b.Title)
//                join ub in _userBookRepository
//                    .GetMany(ub => ub.UserProfileId == userId) on b.BookId equals ub.BookId
//                select b;
//            return books;
//        }

//        public UserProfile GetUserProfileByBookId(string bookId)
//        {
//            var userBook = _userBookRepository.Get(ub => ub.BookId == bookId);
//            return _userProfileRepository.Get(up => up.UserProfileId == userBook.UserProfileId);
//        }

//        public int GetUserBookCount()
//        {
//            return _userBookRepository.GetAll().Count();
//        }

//        private void SaveChanges()
//        {
//            _unitOfWork.Commit();
//        }

//        private void Sorting(ref IEnumerable<UserBook> userBooks, string sorting)
//        {
//            if (string.IsNullOrEmpty(sorting) || sorting.Equals("DateTaken ASC"))
//            {
//                userBooks = userBooks.OrderBy(p => p.DateTaken);
//            }
//            else if (sorting.Equals("DateTaken DESC"))
//            {
//                userBooks = userBooks.OrderByDescending(p => p.DateTaken);
//            }
//            else if (sorting.Equals("DateReturned ASC"))
//            {
//                userBooks = userBooks.OrderBy(p => p.DateReturned);
//            }
//            else if (sorting.Equals("DateReturned DESC"))
//            {
//                userBooks = userBooks.OrderByDescending(p => p.DateReturned);
//            }
//        }
//    }
//}
