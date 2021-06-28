namespace DbDeltaWatcher.Interfaces.Database
{
    public interface ISchemaProvider
    {
        bool TableExists(string tableName);
    }
}