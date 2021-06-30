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
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConnectionStringProvider[] _connectionStringProviders;

        public ConnectionStringProvider(IConnectionStringProvider[] connectionStringProviders)
        {
            _connectionStringProviders = connectionStringProviders;
        }
        
        public IConnectionString GetConnectionStringForName(string connectionStringName)
        {
            foreach (var provider in _connectionStringProviders)
            {
                var temp = provider.GetConnectionStringForName(connectionStringName);
                if (temp != null)
                    return temp;
            }

            return null;
        }
    }
    
    public class Factory : IFactory
    {
        private readonly IConfigurationProvider _configurationProvider;

        public Factory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }
        
        public ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription)
        {
            return connectionDescription.ConnectionType switch
            {
                ConnectionTypeEnum.SqlServer => new SqlServerSchemaProvider(connectionDescription.ConnectionStringName),
                ConnectionTypeEnum.MySql => new MySqlSchemaProvider(connectionDescription.ConnectionStringName),
                _ => throw new ArgumentOutOfRangeException()
            };
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

    public class SqlServerSchemaProvider : ISchemaProvider
    {
        public SqlServerSchemaProvider(string connectionStringName)
        {
            throw new NotImplementedException();
        }

        public bool TableExists(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}