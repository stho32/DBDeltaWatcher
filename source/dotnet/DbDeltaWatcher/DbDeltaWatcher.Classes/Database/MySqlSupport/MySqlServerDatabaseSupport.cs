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
            return new MySqlSchemaProvider(
                _connectionStringProvider.GetConnectionStringFor(connectionDescription));
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            throw new NotImplementedException();
        }
    }
}