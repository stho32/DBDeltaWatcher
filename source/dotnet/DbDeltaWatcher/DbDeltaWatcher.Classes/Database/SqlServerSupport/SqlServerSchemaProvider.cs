using System;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
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

        public ISimplifiedTableSchema GetSimplifiedTableSchema(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}