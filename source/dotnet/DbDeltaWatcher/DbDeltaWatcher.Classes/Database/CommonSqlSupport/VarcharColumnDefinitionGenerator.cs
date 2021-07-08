using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.CommonSqlSupport
{
    public class VarcharColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "varchar" && 
                columnSchema.CharacterMaximumLength != -1)
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()}({columnSchema.CharacterMaximumLength})";
            }

            return null;
        }
    }
}