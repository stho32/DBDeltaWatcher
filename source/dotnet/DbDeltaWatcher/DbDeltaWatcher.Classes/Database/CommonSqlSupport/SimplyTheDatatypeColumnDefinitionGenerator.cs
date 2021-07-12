using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.CommonSqlSupport
{
    public class SimplyTheDatatypeColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        private readonly string _datatypeName;

        public SimplyTheDatatypeColumnDefinitionGenerator(string datatypeName)
        {
            _datatypeName = datatypeName;
        }
        
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema, bool includeName)
        {
            if (columnSchema.DataType.ToLower() == _datatypeName.ToLower())
            {
                return (includeName ? $"{columnSchema.ColumnName} ":"") + 
                       $"{_datatypeName.ToUpper()}";
            }

            return null;
        }
    }
}