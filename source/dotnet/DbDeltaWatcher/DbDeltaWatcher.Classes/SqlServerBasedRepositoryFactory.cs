using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Repositories;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes
{
    public class SqlServerBasedRepositoryFactory : IRepositoryFactory
    {
        private readonly IConfigurationProvider _configurationProvider;

        public SqlServerBasedRepositoryFactory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public ITaskRepository TaskRepository => new TaskRepository(new SqlServerDatabaseConnection(_configurationProvider.GetMasterConnectionString()));
        public ITaskProcessor CreateTaskProcessor(ITask task)
        {
            // create source and so on
            return new TaskProcessor();
        }
    }
}