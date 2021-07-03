namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISchemaProviderFactory
    {
        ISchemaProvider GetSchemaProviderFor(IConnectionDescription connectionDescription);
    }
}