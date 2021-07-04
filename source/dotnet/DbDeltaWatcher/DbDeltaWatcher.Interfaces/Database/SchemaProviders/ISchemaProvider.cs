namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISchemaProvider
    {
        bool TableExists(string tableName);
        ISimplifiedTableSchema GetSimplifiedTableSchema(string tableName);
    }
}