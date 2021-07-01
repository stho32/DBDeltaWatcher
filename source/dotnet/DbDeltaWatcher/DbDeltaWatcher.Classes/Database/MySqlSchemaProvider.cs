using System;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    public class MySqlSchemaProvider : ISchemaProvider
    {
        public MySqlSchemaProvider(IConnectionString connectionString)
        {
            
        }
        
        public bool TableExists(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}