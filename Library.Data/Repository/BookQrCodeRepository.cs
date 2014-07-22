using Library.Data.Infrastructure;
using Library.Model.Models;

namespace Library.Data.Repository
{
    public class BookQrCodeRepository : RepositoryBase<BookQrCode>, IBookQrCodeRepository
    {
        public BookQrCodeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IBookQrCodeRepository : IRepository<BookQrCode>
    {
    }
}
