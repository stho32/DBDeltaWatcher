using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerSchemaProvider : SchemaProviderBase
    {
        public SqlServerSchemaProvider(IDatabaseConnection connection) : base(connection)
        {
        }
    }
}