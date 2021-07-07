using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.BL.Tests
{
    public interface ISqlDialect
    {
        string CreateTableStart(string tableName);
        string CreateTableEnd();
        string ColumnDefinition(ISimplifiedColumnSchema columnSchema);
    }

    public interface IColumnDefinitionGenerator
    {
        string GetColumnDefinition(ISimplifiedColumnSchema columnSchema);
    }

    public class MySqlPrimaryKeyColumnDefinitionGenerator : IColumnDefinitionGenerator
    {
        public string GetColumnDefinition(ISimplifiedColumnSchema columnSchema)
        {
            if (columnSchema.DataType.ToLower() == "int" &&
                columnSchema.IsPrimaryKey)
            {
                return $"{columnSchema.ColumnName} {columnSchema.DataType} NOT NULL AUTO_INCREMENT PRIMARY KEY";
            }

            return null;
        }
    }

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
            return ")";
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

    public class MySqlDialect : SqlDialectBase
    {
        public MySqlDialect() : base(
            new IColumnDefinitionGenerator[]
            {
                new MySqlPrimaryKeyColumnDefinitionGenerator()
            })
        {
        }
    }
}