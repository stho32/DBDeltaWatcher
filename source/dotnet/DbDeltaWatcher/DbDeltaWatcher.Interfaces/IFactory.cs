using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Interfaces
{
    public interface IFactory
    {
        ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription);
        IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription);
        IRepositoryFactory RepositoryFactory();
        ITaskProcessor CreateTaskProcessor(ITask task, IFactory factory);
    }
}