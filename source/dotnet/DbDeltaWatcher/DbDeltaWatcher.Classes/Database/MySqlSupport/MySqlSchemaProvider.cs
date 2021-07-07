using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlSchemaProvider : SchemaProviderBase
    {
        public MySqlSchemaProvider(IDatabaseConnection connection) : base(connection)
        {
        }
    }
}