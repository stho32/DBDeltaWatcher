using System;
using DbDeltaWatcher.Interfaces.Configuration;

namespace DbDeltaWatcher.Classes.Configuration
{
    /// <summary>
    /// A class that uses 1..n different configuration sources to provide
    /// the configuration information 
    /// </summary>
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfigurationProvider[] _configurationProviders;

        public ConfigurationProvider(IConfigurationProvider[] configurationProviders)
        {
            _configurationProviders = configurationProviders;
        }
        
        public string GetMasterConnectionString()
        {
            foreach (var configurationProvider in _configurationProviders)
            {
                var value = configurationProvider.GetMasterConnectionString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            throw new Exception("Missing configuration value for master connection string.");
        }
    }
}