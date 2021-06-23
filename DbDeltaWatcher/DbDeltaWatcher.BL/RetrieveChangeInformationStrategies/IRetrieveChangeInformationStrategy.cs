using System.Data;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.BL.Interfaces
{
    public interface IRetrieveChangeInformationStrategy
    {
        IChangeInformation[] GetChanges(ITask task);
    }

    
    
    public interface IChangeInformation
    {
        DataRow OldRow { get; }
        DataRow NewRow { get; }
        
        
    }
}