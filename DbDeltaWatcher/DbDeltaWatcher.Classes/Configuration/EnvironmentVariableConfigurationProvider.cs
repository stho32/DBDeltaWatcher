using System;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Configuration
{
    /// <summary>
    /// A Configuration Provider that looks for environment variables
    /// </summary>
    public class EnvironmentVariableConfigurationProvider : IConfigurationProvider
    {
        public string GetMasterConnectionString()
        {
            return Environment.GetEnvironmentVariable("DBDELTAWATCHERCONNECTION");
        }
    }
}