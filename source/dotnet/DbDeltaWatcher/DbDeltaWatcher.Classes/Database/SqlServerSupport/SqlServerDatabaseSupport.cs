using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerDatabaseSupport : IDatabaseSupport
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public SqlServerDatabaseSupport(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        public bool IsSupportFor(IConnectionDescription connectionDescription)
        {
            return connectionDescription.ConnectionType == ConnectionTypeEnum.SqlServer;
        }

        public ISchemaProvider GetSchemaProvider(IConnectionDescription connectionDescription)
        {
            var connection = GetDatabaseConnection(connectionDescription);
            return new SqlServerSchemaProvider(connection);
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            var connectionString = _connectionStringProvider.GetConnectionStringFor(connectionDescription);
            if (string.IsNullOrWhiteSpace(connectionString?.Value))
            {
                CONTINUE HERE
            }
            return new SqlServerDatabaseConnection(connectionString);
        }

        public ISqlDialect GetSqlDialect(IConnectionDescription connectionDescription)
        {
            return new SqlServerDialect();
        }
    }
}