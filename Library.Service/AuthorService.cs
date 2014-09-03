using System;
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
    public interface IAuthorService
    {
        IEnumerable<AuthorModel> GetAuthors();
        IEnumerable<AuthorModel> GetAuthors(string sorting);
        IEnumerable<AuthorModel> GetAuthors(int startIndex, int count);
        IEnumerable<AuthorModel> GetAuthors(int startIndex, int count, string sorting);
        IEnumerable<AuthorModel> GetAuthors(int count, string filtering);
        AuthorModel GetAuthor(string id);
        string CreateAuthor(AuthorModel author);
        void UpdateAuthor(AuthorModel author);
        void DeleteAuthor(string id);
        int GetAuthorsCount();
        void Sorting(ref IEnumerable<AuthorModel> authors, string sorting);
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

        public IEnumerable<AuthorModel> GetAuthors()
        {
            IEnumerable<Author> authors = _authorRepository.GetAll();
            List<AuthorModel> authorModels = authors.Select(Mapper.Map<Author, AuthorModel>).ToList();
            return authorModels;
        }

        public IEnumerable<AuthorModel> GetAuthors(string sorting)
        {
            IEnumerable<Author> authors = _authorRepository.GetAll();
            IEnumerable<AuthorModel> authorModels = authors.Select(Mapper.Map<Author, AuthorModel>).ToList();
            Sorting(ref authorModels, sorting);
            return authorModels;
        }

        public IEnumerable<AuthorModel> GetAuthors(int startIndex, int count)
        {
            IEnumerable<Author> authors = _authorRepository.GetAll().Skip(startIndex).Take(count).ToList();
            IEnumerable<AuthorModel> authorModels = authors.Select(Mapper.Map<Author, AuthorModel>).ToList();
            return authorModels;
        }

        public IEnumerable<AuthorModel> GetAuthors(int startIndex, int count, string sorting)
        {
            IEnumerable<Author> authors = _authorRepository.GetAll().Skip(startIndex).Take(count).ToList();
            IEnumerable<AuthorModel> authorModels = authors.Select(Mapper.Map<Author, AuthorModel>).ToList();
            Sorting(ref authorModels, sorting);
            return authorModels;
        }

        public IEnumerable<AuthorModel> GetAuthors(int count, string filtering)
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
            resultAuthors = resultAuthors.Take(count);
            IEnumerable<AuthorModel> authorModels = resultAuthors.Select(Mapper.Map<Author, AuthorModel>).ToList();
            return authorModels;
        }


        public AuthorModel GetAuthor(string id)
        {
            var author = _authorRepository.GetById(id);
            return Mapper.Map<Author,AuthorModel>(author);
        }

        public string CreateAuthor(AuthorModel authorModel)
        {
            authorModel.AuthorId = UniqueStringKey.GetUniqueKey(LenOfKeyId);
            Author author = Mapper.Map<AuthorModel, Author>(authorModel);
            _authorRepository.Add(author);
            SaveChanges();
            return author.AuthorId;
        }
        public void UpdateAuthor(AuthorModel authorModel)
        {
            Author author = Mapper.Map<AuthorModel, Author>(authorModel);
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

        public void Sorting(ref IEnumerable<AuthorModel> authors, string sorting)
        {
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                authors = authors.OrderBy(p => p.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                authors = authors.OrderByDescending(p => p.Name);
            }
        }
    }
}
