using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Interfaces.Configuration
{
    /// <summary>
    /// Method to get the configuration information
    /// </summary>
    public interface IConfigurationProvider
    {
        ConnectionTypeEnum GetMasterConnectionType(); 
        string GetMasterConnectionString();
    }
}