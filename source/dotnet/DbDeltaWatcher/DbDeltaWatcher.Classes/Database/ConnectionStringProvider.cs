using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConnectionStringProvider[] _connectionStringProviders;

        public ConnectionStringProvider(IConnectionStringProvider[] connectionStringProviders)
        {
            _connectionStringProviders = connectionStringProviders;
        }
        
        public IConnectionString GetConnectionStringFor(IConnectionDescription connectionDescription)
        {
            foreach (var provider in _connectionStringProviders)
            {
                var temp = provider.GetConnectionStringFor(connectionDescription);
                if (temp != null)
                    return temp;
            }

            return null;
        }
    }
}