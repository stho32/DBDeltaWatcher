namespace DbDeltaWatcher.Interfaces.Database
{
    public interface IConnectionType
    {
        int Id { get; }
        string Name { get; }
        string TechnicalIdentifier { get; }
        bool HasConnectionStringName { get; }
        bool HasSql { get; }
        bool IsActive { get; }
    }
}