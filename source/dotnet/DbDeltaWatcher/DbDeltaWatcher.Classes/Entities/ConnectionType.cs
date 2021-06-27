using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class ConnectionType : IConnectionType
    {
        public int Id { get; }
        public string Name { get; }
        public string TechnicalIdentifier { get; }
        public bool HasConnectionStringName { get; }
        public bool HasSql { get; }
        public bool IsActive { get; }

        public ConnectionType(int id, string name, string technicalIdentifier, bool hasConnectionStringName, bool hasSql, bool isActive)
        {
            Id = id;
            Name = name;
            TechnicalIdentifier = technicalIdentifier;
            HasConnectionStringName = hasConnectionStringName;
            HasSql = hasSql;
            IsActive = isActive;
        }
    }
}