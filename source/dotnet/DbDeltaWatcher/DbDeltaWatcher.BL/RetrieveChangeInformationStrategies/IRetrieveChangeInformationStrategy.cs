using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.BL.RetrieveChangeInformationStrategies
{
    public interface IRetrieveChangeInformationStrategy
    {
        IChangeInformation[] GetChanges(ITask task);
    }
}