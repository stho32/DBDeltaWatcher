using System;
using System.Linq;
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
        public ISimplifiedColumnSchema[] GetPrimaryKey()
        {
            var primaryKey = Columns.Where(x => x.IsPrimaryKey).ToArray();
            return primaryKey;
        }

        public ISimplifiedColumnSchema[] GetNonPrimaryKeyColumns()
        {
            return Columns.Where(x => !x.IsPrimaryKey).ToArray();
        }

        public bool ContainsColumnWithName(string columnName)
        {
            var column = GetByName(columnName);
            return column != null;
        }

        public ISimplifiedColumnSchema GetByName(string columnName)
        {
            return Columns.FirstOrDefault(column => string.Equals(column.ColumnName, columnName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}