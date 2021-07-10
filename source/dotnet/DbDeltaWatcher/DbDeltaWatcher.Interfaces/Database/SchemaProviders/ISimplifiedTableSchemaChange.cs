namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISimplifiedTableSchemaChange
    {
        SimplifiedTableSchemaChangeEnum TypeOfChange { get; }
        ISimplifiedColumnSchema ColumnSchema { get; }
    }
}