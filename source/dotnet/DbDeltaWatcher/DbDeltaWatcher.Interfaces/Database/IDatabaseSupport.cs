using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IDatabaseSupport
    {
        bool IsSupportFor(IConnectionDescription connectionDescription);

        ISchemaProvider GetSchemaProvider(IConnectionDescription connectionDescription);
        IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription);
    }
}