using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    public class OneConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConnectionDescription _connectionDescription;
        private readonly IConnectionString _connectionString;

        public OneConnectionStringProvider(
            IConnectionDescription connectionDescription,
            IConnectionString connectionString)
        {
            _connectionDescription = connectionDescription;
            _connectionString = connectionString;
        }
        
        public IConnectionString GetConnectionStringFor(IConnectionDescription connectionDescription)
        {
            if ( connectionDescription.ConnectionType == _connectionDescription.ConnectionType &&
                 connectionDescription.ConnectionStringName.ToLower() == _connectionDescription.ConnectionStringName.ToLower())
                return _connectionString;

            return null;
        }
    }
}