using System;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Configuration
{
    /// <summary>
    /// A class that uses 1..n different configuration sources to provide
    /// the configuration information 
    /// </summary>
    public class ConfigurationProvider : IConfigurationProvider, 
        IConnectionStringProvider
    {
        private readonly IConfigurationProvider[] _configurationProviders;
        private readonly IConnectionStringProvider[] _connectionStringProviders;

        public ConfigurationProvider(
            IConfigurationProvider[] configurationProviders,
            IConnectionStringProvider[] connectionStringProviders)
        {
            _configurationProviders = configurationProviders;
            _connectionStringProviders = connectionStringProviders;
        }

        public ConnectionTypeEnum GetMasterConnectionType()
        {
            return FirstValidProvider().GetMasterConnectionType();
        }

        private IConfigurationProvider _firstValidProvider = null;
        private IConfigurationProvider FirstValidProvider()
        {
            if (_firstValidProvider == null)
            {
                foreach (var configurationProvider in _configurationProviders)
                {
                    var value = configurationProvider.GetMasterConnectionString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return configurationProvider;
                    }
                }
            }
            
            return _firstValidProvider;
        }

        public string GetMasterConnectionString()
        {
            return FirstValidProvider().GetMasterConnectionString();
        }

        public IConnectionString GetConnectionStringForName(string connectionStringName)
        {
            foreach (var provider in _connectionStringProviders)
            {
                var result = provider.GetConnectionStringForName(connectionStringName);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}