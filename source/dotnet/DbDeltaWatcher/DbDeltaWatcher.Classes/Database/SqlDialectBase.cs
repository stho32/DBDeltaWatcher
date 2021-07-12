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

        public string ColumnDefinition(ISimplifiedColumnSchema columnSchema, bool includeName)
        {
            foreach (var generator in _columnDefinitionGenerators)
            {
                var result = generator.GetColumnDefinition(columnSchema, includeName);
                if (!string.IsNullOrWhiteSpace(result))
                    return result;
            }
            return null;
        }

        public ISimplifiedColumnSchema ChecksumColumnSchema()
        {
            return new SimplifiedColumnSchema(
                -1,
                "SourceChecksum",
                "int",
                0,
                10,
                0,
                false);
        }

        public string AddColumnToTable(string tableName, ISimplifiedColumnSchema columnSchema)
        {
            var columnDefinition = ColumnDefinition(columnSchema, true);

            return $"ALTER TABLE {tableName} ADD {columnDefinition};";
        }

        public string RemoveColumnFromTable(string tableName, ISimplifiedColumnSchema columnSchema)
        {
            return $"ALTER TABLE {tableName} DROP COLUMN {columnSchema.ColumnName};";
        }

        public string AlterDataTypeOfColumn(string tableName, ISimplifiedColumnSchema columnSchema)
        {
            var columnDefinition = ColumnDefinition(columnSchema, true);

            return $"ALTER TABLE {tableName} ALTER COLUMN {columnDefinition};";
        }
    }
}