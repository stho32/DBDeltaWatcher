using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    /// <summary>
    /// Connection String Provider that uses a flat text file with a specific structure
    /// </summary>
    public class FlatFileConnectionStringProvider : IConnectionStringProvider 
    {
        private readonly string _pathToFile;
        private readonly string _appName;

        public FlatFileConnectionStringProvider(string pathToFile, string appName)
        {
            _pathToFile = pathToFile;
            _appName = appName;
        }
        
        public IConnectionString GetConnectionStringFor(IConnectionDescription connectionStringName)
        {
            throw new System.NotImplementedException();
        }
    }
}