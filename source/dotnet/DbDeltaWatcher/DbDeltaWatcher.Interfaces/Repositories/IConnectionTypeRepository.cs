using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Interfaces.Repositories
{
    public interface IConnectionTypeRepository
    {
        IConnectionType[] GetList();
        IConnectionType GetById(int id);
    }
}