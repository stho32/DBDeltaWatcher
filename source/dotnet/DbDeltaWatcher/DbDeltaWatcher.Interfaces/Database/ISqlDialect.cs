using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface ISqlDialect
    {
        string CreateTableStart(string tableName);
        string CreateTableEnd();
        string ColumnDefinition(ISimplifiedColumnSchema columnSchema, bool includeName);
        ISimplifiedColumnSchema ChecksumColumnSchema();
        string AddColumnToTable(string tableName, ISimplifiedColumnSchema columnSchema);
        string RemoveColumnFromTable(string tableName, ISimplifiedColumnSchema columnSchema);
        string AlterDataTypeOfColumn(string tableName, ISimplifiedColumnSchema columnSchema);
    }
}