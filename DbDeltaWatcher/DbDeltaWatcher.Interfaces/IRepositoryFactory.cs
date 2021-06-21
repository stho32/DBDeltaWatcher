using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Interfaces
{
    public interface IRepositoryFactory
    {
        ITaskRepository TaskRepository { get; }
    }
}