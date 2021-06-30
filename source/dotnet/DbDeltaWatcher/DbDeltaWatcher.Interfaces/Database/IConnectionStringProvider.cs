namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IConnectionStringProvider
    {
        IConnectionString GetConnectionStringForName(string connectionStringName);
    }

    public interface IConnectionString
    {
        string ConnectionString { get; }
    }
}