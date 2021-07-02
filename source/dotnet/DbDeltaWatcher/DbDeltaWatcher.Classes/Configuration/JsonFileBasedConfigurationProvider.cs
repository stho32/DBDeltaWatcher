using System.IO;
using System.Text.Json;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.Classes.Configuration
{
    public class JsonFileBasedConfigurationProvider : IConfigurationProvider
    {
        private readonly string _filepath;
        private readonly ConfigurationPoco _configuration = new ConfigurationPoco("");

        public class ConfigurationPoco
        {
            public string MasterConnectionString { get; }
            public string MasterConnectionType { get; }

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

        public ConnectionTypeEnum GetMasterConnectionType()
        {
            return _configuration.MasterConnectionType.AsConnectionType();
        }

        public string GetMasterConnectionString()
        {
            return _configuration.MasterConnectionString;
        }

        public void WriteConfiguration()
        {
            var json = JsonSerializer.Serialize(_configuration);
            File.WriteAllText(_filepath, json);
        }
    }
}