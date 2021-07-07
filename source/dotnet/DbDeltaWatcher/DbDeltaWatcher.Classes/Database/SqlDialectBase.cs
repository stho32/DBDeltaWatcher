using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public abstract class SqlDialectBase : ISqlDialect
    {
        private readonly IColumnDefinitionGenerator[] _columnDefinitionGenerators;

        protected SqlDialectBase(IColumnDefinitionGenerator[] columnDefinitionGenerators)
        {
            _columnDefinitionGenerators = columnDefinitionGenerators;
        }
        
        public string CreateTableStart(string tableName)
        {
            return $"CREATE TABLE {tableName} (";
        }

        public string CreateTableEnd()
        {
            return ");";
        }

        public string ColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            foreach (var generator in _columnDefinitionGenerators)
            {
                var result = generator.GetColumnDefinition(columnSchema);
                if (!string.IsNullOrWhiteSpace(result))
                    return result;
            }
            return null;
        }
    }
}