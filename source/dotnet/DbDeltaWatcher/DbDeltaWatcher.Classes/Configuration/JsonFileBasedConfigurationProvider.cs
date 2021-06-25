using System.IO;
using System.Text.Json;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Configuration
{
    public class JsonFileBasedConfigurationProvider : IConfigurationProvider
    {
        private readonly string _filepath;
        private readonly ConfigurationPoco _configuration = new ConfigurationPoco("");

        public class ConfigurationPoco
        {
            public string MasterConnectionString { get; }

            public ConfigurationPoco(string masterConnectionString)
            {
                MasterConnectionString = masterConnectionString;
            }
        }
        
        public JsonFileBasedConfigurationProvider(string filepath)
        {
            _filepath = filepath;

            if (!File.Exists(filepath)) return;
            
            var jsonString = File.ReadAllText(_filepath);
            _configuration = JsonSerializer.Deserialize<ConfigurationPoco>(jsonString);
        }
        
        public string GetMasterConnectionString()
        {
            return _configuration.MasterConnectionString;
        }
    }
}