using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Classes.Repositories;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
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
        private IDatabaseConnection DatabaseConnection => new MySqlServerDatabaseConnection(_connectionString);

    }
}