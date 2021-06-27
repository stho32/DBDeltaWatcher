using System.Data;

namespace DbDeltaWatcher.BL.RetrieveChangeInformationStrategies
{
    public interface IChangeInformation
    {
        ChangeTypeEnum ChangeType { get; }
        DataRow OldState { get; }
        DataRow NewState { get; }
    }
}