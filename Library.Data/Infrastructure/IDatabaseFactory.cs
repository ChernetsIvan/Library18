using System;

namespace Library.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        LibraryEntities Get();
    }
}
