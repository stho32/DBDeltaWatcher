using System;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlSchemaProvider : ISchemaProvider
    {
        private readonly IConnectionString _connectionString;

        public MySqlSchemaProvider(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        
        public bool TableExists(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}