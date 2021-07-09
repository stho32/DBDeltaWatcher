using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.ExtensionMethods
{
    public static class SimplifiedTableSchemaExtensionMethods
    {
        public static ISimplifiedTableSchema DeriveMirrorSchema(this ISimplifiedTableSchema sourceTableSchema,
            string newTableName, ISqlDialect sqlDialect)
        {
            return null;
        }
    }
}