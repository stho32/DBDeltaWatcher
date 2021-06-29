using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Repositories;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes
{
    public class MySqlServerBasedRepositoryFactory : IRepositoryFactory
    {
        private readonly string _connectionString;

        public MySqlServerBasedRepositoryFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ITaskRepository TaskRepository => new TaskRepository(DatabaseConnection);
        public IDatabaseConnection DatabaseConnection => new MySqlServerDatabaseConnection(_connectionString);

        public ITaskProcessor CreateTaskProcessor(ITask task, IFactory factory)
        {
            // create source and so on
            return new TaskProcessor(task, factory);
        }
    }
}