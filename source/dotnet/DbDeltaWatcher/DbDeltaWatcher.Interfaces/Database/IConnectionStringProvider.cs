namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IConnectionStringProvider
    {
        IConnectionString GetConnectionStringFor(IConnectionDescription connectionDescription);
    }

    public interface IConnectionString
    {
        string Value { get; }
    }
}