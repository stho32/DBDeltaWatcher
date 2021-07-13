using System.Data;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IChangeInformation
    {
        ChangeTypeEnum ChangeType { get; }
        DataRow OldState { get; }
        DataRow NewState { get; }
    }
}