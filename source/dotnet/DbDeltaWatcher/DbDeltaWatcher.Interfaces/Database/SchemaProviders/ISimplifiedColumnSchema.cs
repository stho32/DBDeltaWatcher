namespace DbDeltaWatcher.Interfaces.Database.SchemaProviders
{
    public interface ISimplifiedColumnSchema
    {
        public int OrdinalPosition { get; }
        public string ColumnName { get; }
        public string DataType { get; }
        public long CharacterMaximumLength { get; }
        public int NumericPrecision { get; }
        public int NumericScale { get; }
        public bool IsPrimaryKey { get; }
    }
}