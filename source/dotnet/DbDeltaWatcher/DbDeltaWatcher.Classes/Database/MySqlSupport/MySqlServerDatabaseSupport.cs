using System;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlServerDatabaseSupport : IDatabaseSupport
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public MySqlServerDatabaseSupport(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        
        public bool IsSupportFor(IConnectionDescription connectionDescription)
        {
            return connectionDescription.ConnectionType == ConnectionTypeEnum.MySql;
        }

        public ISchemaProvider GetSchemaProvider(IConnectionDescription connectionDescription)
        {
            var connection = GetDatabaseConnection(connectionDescription); 
            return new MySqlSchemaProvider(connection);
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            var connectionString = _connectionStringProvider.GetConnectionStringFor(connectionDescription);
            return new MySqlServerDatabaseConnection(connectionString.Value);
        }
    }
}