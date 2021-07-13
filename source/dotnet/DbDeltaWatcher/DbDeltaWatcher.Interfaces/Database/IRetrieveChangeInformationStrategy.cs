using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IRetrieveChangeInformationStrategy
    {
        IChangeInformation[] GetChanges(ITask task);
    }
}