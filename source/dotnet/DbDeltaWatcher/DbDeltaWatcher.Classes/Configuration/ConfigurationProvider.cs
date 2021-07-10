using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Configuration
{
    /// <summary>
    /// A class that uses 1..n different configuration sources to provide
    /// the configuration information 
    /// </summary>
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfigurationProvider[] _configurationProviders;

        public ConfigurationProvider(
            IConfigurationProvider[] configurationProviders)
        {
            _configurationProviders = configurationProviders;
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
            return FirstValidProvider()?.GetMasterConnectionString();
        }
    }
}