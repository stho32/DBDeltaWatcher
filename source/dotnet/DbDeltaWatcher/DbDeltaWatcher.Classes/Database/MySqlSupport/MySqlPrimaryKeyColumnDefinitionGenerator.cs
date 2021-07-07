using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlPrimaryKeyColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "int" &&
                columnSchema.IsPrimaryKey)
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()} NOT NULL PRIMARY KEY AUTO_INCREMENT";
            }

            return null;
        }
    }
}