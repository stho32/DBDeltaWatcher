using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IConnectionDescription
    {
        ConnectionTypeEnum ConnectionType { get; }
        string ConnectionStringName { get; }
    }
}