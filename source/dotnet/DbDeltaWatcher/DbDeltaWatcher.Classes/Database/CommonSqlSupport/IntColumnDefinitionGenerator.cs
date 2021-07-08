using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.CommonSqlSupport
{
    public class IntColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "int")
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()}";
            }

            return null;
        }
    }
}