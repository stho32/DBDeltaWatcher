using System.Data;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.BL.Interfaces
{
    public interface IRetrieveChangeInformationStrategy
    {
        IChangeInformation[] GetChanges(ITask task);
    }

    public enum ChangeTypeEnum
    {
        Added,
        Deleted,
        Changed
    }
    
    public interface IChangeInformation
    {
        ChangeTypeEnum ChangeType { get; }
        DataRow OldState { get; }
        DataRow NewState { get; }
    }
}