using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface ISqlDialect
    {
        string CreateTableStart(string tableName);
        string CreateTableEnd();
        string ColumnDefinition(ISimplifiedColumnSchema columnSchema);
    }
}