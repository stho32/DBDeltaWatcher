namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IConnectionStringProvider
    {
        IConnectionString GetConnectionStringFor(IConnectionDescription connectionStringName);
    }

    public interface IConnectionString
    {
        string ConnectionString { get; }
    }
}