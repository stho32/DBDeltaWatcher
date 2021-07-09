using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.ExtensionMethods
{
    public static class SimplifiedTableSchemaExtensionMethods
    {
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