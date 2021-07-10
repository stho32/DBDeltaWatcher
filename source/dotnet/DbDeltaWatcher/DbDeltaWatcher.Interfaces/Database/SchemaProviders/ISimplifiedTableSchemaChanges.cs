namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISimplifiedTableSchemaChanges
    {
        string TableName { get; }
        ISimplifiedTableSchemaChange[] Changes { get; }
    }
}