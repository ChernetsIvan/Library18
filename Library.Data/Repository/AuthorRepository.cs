using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IAuthorRepository : IRepository<Author>
    {
    }
}
