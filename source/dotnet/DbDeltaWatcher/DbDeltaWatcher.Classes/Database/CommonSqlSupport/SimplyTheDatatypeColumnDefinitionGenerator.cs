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
        
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == _datatypeName.ToLower())
            {
                return $"{columnSchema.ColumnName} {_datatypeName.ToUpper()}";
            }

            return null;
        }
    }
}