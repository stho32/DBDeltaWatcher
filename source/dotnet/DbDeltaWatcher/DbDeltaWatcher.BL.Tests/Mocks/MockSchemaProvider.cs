using System.Linq;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.BL.Tests.SchemaProviders
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
    }
}