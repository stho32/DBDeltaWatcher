using System;
using DbDeltaWatcher.Classes.Configuration;
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

        public Factory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription)
        {
            throw new System.NotImplementedException();
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            throw new System.NotImplementedException();
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
    }
}