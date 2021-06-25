using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Interfaces.Configuration
{
    /// <summary>
    /// Method to get the configuration information
    /// </summary>
    public interface IConfigurationProvider
    {
        string GetMasterConnectionString();
    }
}