using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    public class ConnectionString : IConnectionString
    {
        public ConnectionString(string value)
        {
            Value = value;
        }
        
        public string Value { get; }
    }
}