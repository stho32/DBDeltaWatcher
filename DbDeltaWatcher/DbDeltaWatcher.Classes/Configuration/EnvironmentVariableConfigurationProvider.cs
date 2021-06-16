using System;
using DbDeltaWatcher.Interfaces.Configuration;

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