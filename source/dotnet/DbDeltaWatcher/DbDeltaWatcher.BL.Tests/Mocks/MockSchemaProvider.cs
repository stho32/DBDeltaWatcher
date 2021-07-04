using System;
using System.Linq;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.BL.Tests.Mocks
{
    public class MockSchemaProvider : ISchemaProvider
    {
        private readonly string[] _availableTables;

        public MockSchemaProvider(string[] availableTables)
        {
            _availableTables = availableTables;
        }
        
        public bool TableExists(string tableName)
        {
            return _availableTables.Contains(tableName);
        }

        public ISimplifiedTableSchema GetSimplifiedTableSchema(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}