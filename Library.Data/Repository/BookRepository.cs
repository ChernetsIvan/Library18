using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IBookRepository : IRepository<Book>
    {
    }
}
