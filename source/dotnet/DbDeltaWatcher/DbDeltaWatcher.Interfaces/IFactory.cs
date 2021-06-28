using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Interfaces
{
    public interface IFactory
    {
        ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription);
    }
}