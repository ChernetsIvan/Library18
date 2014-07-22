using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data.Infrastructure;
using Library.Model.Models;
using Library.Data.Repository;
using Library.Core.Utility;

namespace Library.Service
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthors();
        IEnumerable<Author> GetAuthors(string sorting);
        IEnumerable<Author> GetAuthors(int startIndex, int count);
        IEnumerable<Author> GetAuthors(int startIndex, int count, string sorting);
        IEnumerable<Author> GetAuthors(int count, string filtering);
        Author GetAuthor(string id);
        string CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(string id);
        int GetAuthorsCount();
        void Sorting(ref IEnumerable<Author> authors, string sorting);
    }

    public class AuthorService : IAuthorService
    {
        private const int LenOfKeyId = 32;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository,  IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _authorRepository.GetAll();
        }

        public IEnumerable<Author> GetAuthors(string sorting)
        {
            var authors = _authorRepository.GetAll();
            Sorting(ref authors, sorting);
            return authors;
        }

        public IEnumerable<Author> GetAuthors(int startIndex, int count)
        {
            return _authorRepository.GetAll().Skip(startIndex).Take(count).ToList();
        }

        public IEnumerable<Author> GetAuthors(int startIndex, int count, string sorting)
        {
            var authors = _authorRepository.GetAll().Skip(startIndex).Take(count);
            Sorting(ref authors, sorting);
            return authors;
        }

        public IEnumerable<Author> GetAuthors(int count, string filtering)
        {
            IEnumerable<Author> authors = _authorRepository.GetAll();
            IEnumerable<Author> tempAuthors = new List<Author>();
            IEnumerable<Author> resultAuthors = new List<Author>();

            if (authors != null)
            {
                tempAuthors = authors.Where(p => ((p.LastName + " " + p.Name).IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((p.Name + " " + p.LastName).IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0));
                (resultAuthors as List<Author>).AddRange(tempAuthors);
                if (resultAuthors.Count() < count)
                {
                    tempAuthors = authors.Where(p => p.LastName.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                    (resultAuthors as List<Author>).AddRange(tempAuthors);
                    resultAuthors = resultAuthors.Distinct().ToList();
                }
                if (resultAuthors.Count() < count)
                {
                    tempAuthors = authors.Where(p => p.Name.IndexOf(filtering, StringComparison.OrdinalIgnoreCase) >= 0);
                    (resultAuthors as List<Author>).AddRange(tempAuthors);
                    resultAuthors = resultAuthors.Distinct().ToList();
                }
            }
            return resultAuthors.Take(count);
        }

        
        public Author GetAuthor(string id)
        {
            var author = _authorRepository.GetById(id);
            return author;
        }

        public string CreateAuthor(Author author)
        {
            author.AuthorId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            _authorRepository.Add(author);
            SaveChanges();
            return author.AuthorId;
        }
        public void UpdateAuthor(Author author)
        {
            _authorRepository.Update(author);
            SaveChanges();
        }

        public void DeleteAuthor(string id)
        {
            var author = _authorRepository.GetById(id);
            _authorRepository.Delete(author);
            SaveChanges();
        }

        public int GetAuthorsCount()
        {
            return _authorRepository.GetAll().Count();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Sorting(ref IEnumerable<Author> authors, string sorting)
        {
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                authors = authors.OrderBy(p => p.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                authors = authors.OrderByDescending(p => p.Name);
            }
            else if (sorting.Equals("LastName ASC"))
            {
                authors = authors.OrderBy(p => p.LastName);
            }
            else if (sorting.Equals("LastName DESC"))
            {
                authors = authors.OrderByDescending(p => p.LastName);
            }
        }
    }
}
