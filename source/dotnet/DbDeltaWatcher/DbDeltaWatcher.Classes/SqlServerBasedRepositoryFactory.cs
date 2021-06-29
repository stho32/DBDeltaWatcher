using DbDeltaWatcher.Classes.Database;
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

        public ITaskRepository TaskRepository => new TaskRepository(new SqlServerDatabaseConnection(_masterConnectionString));
        public ITaskProcessor CreateTaskProcessor(ITask task, IFactory factory)
        {
            // create source and so on
            return new TaskProcessor(task, factory);
        }
    }
}