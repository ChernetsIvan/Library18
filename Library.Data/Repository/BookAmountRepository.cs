using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class BookAmountRepository : RepositoryBase<BookAmount>, IBookAmountRepository
    {
        public BookAmountRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IBookAmountRepository : IRepository<BookAmount>
    {
    }
}
