using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.SqlServerSupport;
using DbDeltaWatcher.Classes.Repositories;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes
{
    public class SqlServerBasedRepositoryFactory : IRepositoryFactory
    {
        private readonly string _masterConnectionString;

        public SqlServerBasedRepositoryFactory(string masterConnectionString)
        {
            _masterConnectionString = masterConnectionString;
        }

        public ITaskRepository TaskRepository => new TaskRepository(
            new SqlServerDatabaseConnection(new ConnectionString(_masterConnectionString))
            );
    }
}