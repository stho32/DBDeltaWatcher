using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlDecimalColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "decimal")
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()}({columnSchema.NumericPrecision},{columnSchema.NumericScale})";
            }

            return null;
        }
    }
}