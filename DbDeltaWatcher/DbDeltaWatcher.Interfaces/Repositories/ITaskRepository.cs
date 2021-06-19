using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        ITask[] GetList();
        ITask GetById(int id);
    }
}