using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class SimplifiedTableSchemaChange : ISimplifiedTableSchemaChange
    {
        public SimplifiedTableSchemaChangeEnum TypeOfChange { get; }
        public ISimplifiedColumnSchema ColumnSchema { get; }

        public SimplifiedTableSchemaChange(SimplifiedTableSchemaChangeEnum typeOfChange, ISimplifiedColumnSchema columnSchema)
        {
            TypeOfChange = typeOfChange;
            ColumnSchema = columnSchema;
        }
    }
}