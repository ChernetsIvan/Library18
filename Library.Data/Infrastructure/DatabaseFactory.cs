namespace Library.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private LibraryEntities dataContext;
    public LibraryEntities Get()
    {
        return dataContext ?? (dataContext = new LibraryEntities());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
