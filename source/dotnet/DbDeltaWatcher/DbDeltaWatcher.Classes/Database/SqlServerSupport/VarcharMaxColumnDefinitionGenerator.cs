using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class VarcharMaxColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema,
            bool includeName)
        {
            if (columnSchema.DataType.ToLower() == "varchar" &&
                columnSchema.CharacterMaximumLength == -1)
            {
                return (includeName?$"{columnSchema.ColumnName} ":"") + 
                       $"{columnSchema.DataType.ToUpper()}(MAX)";
            }

            return null;
        }
    }
}