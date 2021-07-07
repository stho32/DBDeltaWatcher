using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlVarcharColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "varchar")
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()}({columnSchema.CharacterMaximumLength})";
            }

            return null;
        }
    }
}