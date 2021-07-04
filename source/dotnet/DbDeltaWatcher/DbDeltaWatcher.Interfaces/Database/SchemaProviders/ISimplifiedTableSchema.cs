namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISimplifiedTableSchema
    {
        string TableName { get; }
        ISimplifiedColumnSchema[] Columns { get; }
    }
}