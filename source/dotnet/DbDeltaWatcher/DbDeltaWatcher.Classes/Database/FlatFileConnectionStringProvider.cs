using System.Collections.Generic;
using System.IO;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    /// <summary>
    /// Connection String Provider that uses a flat text file with a specific structure
    /// </summary>
    public class FlatFileConnectionStringProvider : IConnectionStringProvider 
    {
        private readonly string _pathToFile;
        private readonly string _appName;
        private Dictionary<string, string> _cache;

        public FlatFileConnectionStringProvider(string pathToFile, string appName)
        {
            _pathToFile = pathToFile;
            _appName = appName;
        }

        private void PopulateCache()
        {
            if (_cache != null) return;
            
            _cache = new Dictionary<string, string>();
            var rows = File.ReadAllLines(_pathToFile);
            foreach (var row in rows)
            {
                if (row.Trim().StartsWith("#"))
                    continue;

                var split = row.Split('=', 2);
                if (split.Length == 2)
                {
                    var key = split[0];
                    key = key.ToLower();
                    if (!_cache.ContainsKey(key))
                    {
                        _cache.Add(key, split[1].Trim());
                    }
                }
            }
        }        
        
        public IConnectionString GetConnectionStringFor(IConnectionDescription connectionDescription)
        {
            var typeName = "ConnectionString";
            
            var value = GetValueFor(typeName,connectionDescription.ConnectionStringName, _appName);
            if (!string.IsNullOrWhiteSpace(value))
                return new ConnectionString(value);
            
            value = GetValueFor(typeName, connectionDescription.ConnectionStringName);
            if (!string.IsNullOrWhiteSpace(value))
                return new ConnectionString(value);

            return null;
        }

        private string GetValueFor(string valueType, string valueName, string appName = "")
        {
            PopulateCache();

            var key = valueType + "|" + valueName;
            if (!string.IsNullOrWhiteSpace(appName)) key += "|" + appName;

            key = key.ToLower();
            
            return _cache.TryGetValue(key, out string value) ? value : null;
        }
    }
}