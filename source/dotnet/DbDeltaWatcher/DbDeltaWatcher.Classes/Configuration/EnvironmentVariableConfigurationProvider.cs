using System;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Configuration
{
    /// <summary>
    /// A Configuration Provider that looks for environment variables
    /// </summary>
    public class EnvironmentVariableConfigurationProvider : IConfigurationProvider
    {
        public ConnectionTypeEnum GetMasterConnectionType()
        {
            return Environment.GetEnvironmentVariable("DBDELTAWATCHERCONNECTIONTYPE").AsConnectionType();
        }

        public string GetMasterConnectionString()
        {
            return Environment.GetEnvironmentVariable("DBDELTAWATCHERCONNECTION");
        }
    }
}