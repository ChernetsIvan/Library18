using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class UserBookRepository : RepositoryBase<UserBook>, IUserBookRepository
    {
        public UserBookRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IUserBookRepository : IRepository<UserBook>
    {
    }
}
