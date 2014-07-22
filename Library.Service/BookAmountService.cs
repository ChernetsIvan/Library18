using System.Collections.Generic;
using System.Linq;
using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Core.Utility;

namespace Library.Service
{
    public interface IBookAmountService
    {
        IEnumerable<BookAmount> GetBookAmounts();
        IEnumerable<BookAmount> GetBookAmounts(string sorting);
        IEnumerable<BookAmount> GetBookAmounts(int startIndex, int count);
        IEnumerable<BookAmount> GetBookAmounts(int startIndex, int count, string sorting);
        BookAmount GetBookAmount(string id);
        string CreateBookAmount(BookAmount bookAmount);
        void UpdateBookAmount(BookAmount bookAmount);
        void DeleteBookAmount(string id);
        int GetBookAmountsCount();
        void Sorting(ref IEnumerable<BookAmount> bookAmounts, string sorting);
    }

    public class BookAmountService : IBookAmountService
    {
        private const int LenOfKeyId = 32;
        private readonly IBookAmountRepository _bookAmountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookAmountService(IBookAmountRepository bookAmountRepository, IUnitOfWork unitOfWork)
        {
            _bookAmountRepository = bookAmountRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BookAmount> GetBookAmounts()
        {
            return _bookAmountRepository.GetAll();
        }

        public IEnumerable<BookAmount> GetBookAmounts(string sorting)
        {
            var bookAmounts = _bookAmountRepository.GetAll();
            Sorting(ref bookAmounts, sorting);
            return bookAmounts;
        }

        public IEnumerable<BookAmount> GetBookAmounts(int startIndex, int count)
        {
            return _bookAmountRepository.GetAll().Skip(startIndex).Take(count).ToList();
        }

        public IEnumerable<BookAmount> GetBookAmounts(int startIndex, int count, string sorting)
        {
            var bookAmounts = _bookAmountRepository.GetAll().Skip(startIndex).Take(count);
            Sorting(ref bookAmounts, sorting);
            return bookAmounts;
        }

        public BookAmount GetBookAmount(string id)
        {
            var bookAmount = _bookAmountRepository.GetById(id);
            return bookAmount;
        }

        public string CreateBookAmount(BookAmount bookAmount)
        {
            bookAmount.BookAmountId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            _bookAmountRepository.Add(bookAmount);
            SaveChanges();
            return bookAmount.BookAmountId;
        }
        public void UpdateBookAmount(BookAmount bookAmount)
        {
            _bookAmountRepository.Update(bookAmount);
            SaveChanges();
        }

        public void DeleteBookAmount(string id)
        {
            var bookAmount = _bookAmountRepository.GetById(id);
            _bookAmountRepository.Delete(bookAmount);
            SaveChanges();
        }

        public int GetBookAmountsCount()
        {
            return _bookAmountRepository.GetAll().Count();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Sorting(ref IEnumerable<BookAmount> bookAmounts, string sorting)
        {
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Amount ASC"))
            {
                bookAmounts = bookAmounts.OrderBy(p => p.Amount);
            }
            else if (sorting.Equals("Amount DESC"))
            {
                bookAmounts = bookAmounts.OrderByDescending(p => p.Amount);
            }
        }
    }
}
