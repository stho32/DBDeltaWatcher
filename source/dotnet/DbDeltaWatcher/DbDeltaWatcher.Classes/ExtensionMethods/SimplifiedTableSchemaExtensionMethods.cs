using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.ExtensionMethods
{
    public static class SimplifiedTableSchemaExtensionMethods
    {
        public static ISimplifiedTableSchemaChanges PrepareMigrationTo(
            this ISimplifiedTableSchema sourceSchema,
            ISimplifiedTableSchema targetSchema)
        {
            var changes = new List<ISimplifiedTableSchemaChange>();
            
            // When I cannot find the source-column in the target schema, then
            // it is something that I need to add.
            for (int i = 0; i < sourceSchema.Columns.Length; i++)
            {
                if (targetSchema.ContainsColumnWithName(sourceSchema.Columns[i].ColumnName))
                    continue;
                
                changes.Add(
                    new SimplifiedTableSchemaChange(SimplifiedTableSchemaChangeEnum.AddColumn,
                    sourceSchema.Columns[i]));
            }
            
            // When I look through the target scheme and I find a column that 
            // is not present in the source, then it needs to be dropped
            for (int i = 0; i < targetSchema.Columns.Length; i++)
            {
                if (sourceSchema.ContainsColumnWithName(targetSchema.Columns[i].ColumnName))
                    continue;
                
                changes.Add(
                    new SimplifiedTableSchemaChange(SimplifiedTableSchemaChangeEnum.RemoveColumn,
                        targetSchema.Columns[i]));
            }
            
            // When I compare all columns that both scheme definitions have in common
            // and while the name is the same I find out that the data types are
            // different, then I need to update the data type within the target
            // to the source data type definition.
            for (int i = 0; i < targetSchema.Columns.Length; i++)
            {
                // This time I am only looking for columns that are available in both schemes
                var sourceColumn = sourceSchema.GetByName(targetSchema.Columns[i].ColumnName);
                if (sourceColumn == null)
                    continue;

                if (!sourceColumn.DataTypeDefinitionEquals(targetSchema.Columns[i]))
                {
                    changes.Add(
                        new SimplifiedTableSchemaChange(SimplifiedTableSchemaChangeEnum.ChangeDataType,
                            sourceColumn));
                }
            }
            
            return new SimplifiedTableSchemaChanges(
                targetSchema.TableName,
                changes.ToArray()
                );
        }
        
        public static ISimplifiedTableSchema DeriveMirrorSchema(
            this ISimplifiedTableSchema sourceTableSchema,
            string newTableName, 
            ISqlDialect sqlDialect)
        {
            var mirroredColumns = new List<ISimplifiedColumnSchema>();

            var primaryKeys = sourceTableSchema.GetPrimaryKey();
            foreach (var primaryKey in primaryKeys)
            {
                mirroredColumns.Add(DerivedColumn(mirroredColumns.Count, primaryKey, "Mirrored"));
            }

            var checksumColumn = sqlDialect.ChecksumColumnSchema();
            mirroredColumns.Add(DerivedColumn(mirroredColumns.Count, checksumColumn, ""));

            var remainingColumns = sourceTableSchema.GetNonPrimaryKeyColumns();
            foreach (var columnSchema in remainingColumns)
            {
                mirroredColumns.Add(DerivedColumn(mirroredColumns.Count, columnSchema, "Old_"));
            }

            return new SimplifiedTableSchema(newTableName, mirroredColumns.ToArray());
        }

        private static SimplifiedColumnSchema DerivedColumn(int position, ISimplifiedColumnSchema columnSchema,
            string newPrefix)
        {
            return new SimplifiedColumnSchema(
                position,
                newPrefix + columnSchema.ColumnName,
                columnSchema.DataType,
                columnSchema.CharacterMaximumLength,
                columnSchema.NumericPrecision,
                columnSchema.NumericScale,
                columnSchema.IsPrimaryKey);
        }
    }
}