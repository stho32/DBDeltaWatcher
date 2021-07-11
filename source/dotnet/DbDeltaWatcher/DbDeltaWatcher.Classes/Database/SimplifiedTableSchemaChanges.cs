using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class SimplifiedTableSchemaChanges : ISimplifiedTableSchemaChanges
    {
        public string TableName { get; }
        public ISimplifiedTableSchemaChange[] Changes { get; }

        public SimplifiedTableSchemaChanges(string tableName, ISimplifiedTableSchemaChange[] changes)
        {
            TableName = tableName;
            Changes = changes;
        }
    }
}