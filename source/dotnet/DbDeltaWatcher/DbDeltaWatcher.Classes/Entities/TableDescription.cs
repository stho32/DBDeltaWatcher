using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class TableDescription : ITableDescription
    {
        public string TableName { get; }

        public TableDescription(string tableName)
        {
            TableName = tableName;
        }
    }
}