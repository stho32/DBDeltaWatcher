using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerBitColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "bit")
            {
                return $"{columnSchema.ColumnName} BIT";
            }

            return null;
        }
    }
}