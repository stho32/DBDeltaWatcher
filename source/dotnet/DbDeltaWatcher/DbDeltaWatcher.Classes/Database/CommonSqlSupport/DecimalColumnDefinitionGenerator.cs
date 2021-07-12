using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.CommonSqlSupport
{
    public class DecimalColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema, bool includeName)
        {
            if (columnSchema.DataType.ToLower() == "decimal")
            {
                var result = "";
                if (includeName)
                {
                    result = $"{columnSchema.ColumnName} ";
                }
                return result + $"{columnSchema.DataType.ToUpper()}({columnSchema.NumericPrecision},{columnSchema.NumericScale})";
            }

            return null;
        }
    }
}