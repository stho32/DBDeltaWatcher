using System;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class SimplifiedColumnSchema : ISimplifiedColumnSchema
    {
        public int OrdinalPosition { get; }
        public string ColumnName { get; }
        public string DataType { get; }
        public long CharacterMaximumLength { get; }
        public int NumericPrecision { get; }
        public int NumericScale { get; }
        public bool IsPrimaryKey { get; }
        public bool DataTypeDefinitionEquals(ISimplifiedColumnSchema targetSchemaColumn)
        {
            return
                string.Equals(DataType, targetSchemaColumn.DataType, StringComparison.CurrentCultureIgnoreCase) &&
                CharacterMaximumLength == targetSchemaColumn.CharacterMaximumLength &&
                NumericPrecision == targetSchemaColumn.NumericPrecision &&
                NumericScale == targetSchemaColumn.NumericScale &&
                IsPrimaryKey == targetSchemaColumn.IsPrimaryKey;
        }

        public SimplifiedColumnSchema(int ordinalPosition,
            string columnName,
            string dataType,
            long characterMaximumLength,
            int numericPrecision,
            int numericScale, 
            bool isPrimaryKey)
        {
            OrdinalPosition = ordinalPosition;
            ColumnName = columnName;
            DataType = dataType;
            CharacterMaximumLength = characterMaximumLength;
            NumericPrecision = numericPrecision;
            NumericScale = numericScale;
            IsPrimaryKey = isPrimaryKey;
        }
    }
}