using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IColumnDefinitionGenerator
    {
        string GetColumnDefinition(ISimplifiedColumnSchema columnSchema);
    }
}