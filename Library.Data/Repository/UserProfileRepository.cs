using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }
    }

    public interface IUserProfileRepository : IRepository<UserProfile>
    {    
    }
}

