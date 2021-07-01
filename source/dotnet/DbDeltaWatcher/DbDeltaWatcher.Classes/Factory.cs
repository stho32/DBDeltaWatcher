using System;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes
{
    public class Factory : IFactory
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IConnectionStringProvider _connectionStringProvider;

        public Factory(
            IConfigurationProvider configurationProvider,
            IConnectionStringProvider connectionStringProvider)
        {
            _configurationProvider = configurationProvider;
            _connectionStringProvider = connectionStringProvider;
        }
        
        public ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription)
        {
            var connectionString = GetConnectionStringFor(connectionDescription);
            
            return connectionDescription.ConnectionType switch
            {
                ConnectionTypeEnum.SqlServer => new SqlServerSchemaProvider(connectionString),
                ConnectionTypeEnum.MySql => new MySqlSchemaProvider(connectionString),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IConnectionString GetConnectionStringFor(IConnectionDescription connectionDescription)
        {
            return _connectionStringProvider.GetConnectionStringFor(connectionDescription);
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            return connectionDescription.ConnectionType switch
            {
                ConnectionTypeEnum.SqlServer => new SqlServerDatabaseConnection(connectionDescription
                    .ConnectionStringName),
                ConnectionTypeEnum.MySql => new MySqlServerDatabaseConnection(
                    connectionDescription.ConnectionStringName),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IRepositoryFactory RepositoryFactory()
        {
            switch (_configurationProvider.GetMasterConnectionType())
            {
                case ConnectionTypeEnum.SqlServer:
                    return new SqlServerBasedRepositoryFactory(_configurationProvider.GetMasterConnectionString());
                case ConnectionTypeEnum.MySql:
                    return new MySqlServerBasedRepositoryFactory(_configurationProvider.GetMasterConnectionString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ITaskProcessor CreateTaskProcessor(ITask task, IFactory factory)
        {
            return new TaskProcessor(task, factory);
        }

        public IConnectionStringProvider GetConnectionStringProvider()
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}