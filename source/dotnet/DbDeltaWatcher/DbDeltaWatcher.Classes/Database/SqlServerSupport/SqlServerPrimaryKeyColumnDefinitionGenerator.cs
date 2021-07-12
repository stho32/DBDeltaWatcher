using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerPrimaryKeyColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema, bool includeName)
        {
            if (columnSchema.DataType.ToLower() == "int" &&
                columnSchema.IsPrimaryKey)
            {
                return (includeName?$"{columnSchema.ColumnName} ":"") + 
                       $"{columnSchema.DataType.ToUpper()} NOT NULL PRIMARY KEY IDENTITY";
            }

            return null;
        }
    }
}