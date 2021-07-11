namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISimplifiedTableSchema
    {
        string TableName { get; }
        ISimplifiedColumnSchema[] Columns { get; }
        ISimplifiedColumnSchema[] GetPrimaryKey();
        ISimplifiedColumnSchema[] GetNonPrimaryKeyColumns();
        bool ContainsColumnWithName(string columnName);
        ISimplifiedColumnSchema GetByName(string columnName);
    }
}