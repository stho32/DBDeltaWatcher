using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlMediumtextColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "mediumtext")
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType.ToUpper()}";
            }

            return null;
        }
    }
}