using System;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    public class SqlServerSchemaProvider : ISchemaProvider
    {
        public SqlServerSchemaProvider(IConnectionString connectionString)
        {
            throw new NotImplementedException();
        }

        public bool TableExists(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}