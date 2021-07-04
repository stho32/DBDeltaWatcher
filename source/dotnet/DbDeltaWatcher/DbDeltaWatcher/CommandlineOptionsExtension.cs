using System;
using System.Collections.Generic;
using System.IO;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher
{
    public static class CommandlineOptionsExtension
    {
        public static IConnectionStringProvider CreateConnectionStringProvider(this CommandLineOptions options, string appName)
        {
            var providers = new List<IConnectionStringProvider>();

            if (!string.IsNullOrWhiteSpace(options.FlatFileConnectionStringProviderFilePath))
            {
                providers.Add(new FlatFileConnectionStringProvider(options.FlatFileConnectionStringProviderFilePath, appName));
            }

            return new ConnectionStringProvider(providers.ToArray());
        }
        
        public static IConfigurationProvider CreateConfigurationProvider(this CommandLineOptions options)
        {
            var filePath = options.ConfigFilePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = Path.Join(AppContext.BaseDirectory, "config.json");
            }
            
            return new ConfigurationProvider(
                new IConfigurationProvider[]
                {
                    new EnvironmentVariableConfigurationProvider(),
                    new JsonFileBasedConfigurationProvider(filePath)
                }
            );
        }
    }
}