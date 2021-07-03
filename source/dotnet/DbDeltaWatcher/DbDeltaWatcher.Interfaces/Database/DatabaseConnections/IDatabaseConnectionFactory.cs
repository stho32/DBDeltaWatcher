namespace DbDeltaWatcher.Interfaces.Database.DatabaseConnections
{
    public interface IDatabaseConnectionFactory
    {
        IDatabaseConnection GetDatabaseConnectionFor(IConnectionDescription connectionDescription);
    }
}