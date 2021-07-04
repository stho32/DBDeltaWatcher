using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class SimplifiedTableSchema : ISimplifiedTableSchema
    {
        public SimplifiedTableSchema(string tableName, ISimplifiedColumnSchema[] columns)
        {
            Columns = columns;
            TableName = tableName;
        }

        public string TableName { get; }
        public ISimplifiedColumnSchema[] Columns { get; }
    }
}