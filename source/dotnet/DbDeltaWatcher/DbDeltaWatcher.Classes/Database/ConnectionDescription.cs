using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Database
{
    public class ConnectionDescription : IConnectionDescription
    {
        public ConnectionTypeEnum ConnectionType { get; }
        public string ConnectionStringName { get; }

        public ConnectionDescription(ConnectionTypeEnum connectionType, string connectionStringName)
        {
            ConnectionType = connectionType;
            ConnectionStringName = connectionStringName;
        }
    }
}