namespace DbDeltaWatcher.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        
    }

    public interface ITask
    {
        int Id { get; }
        string ProcessName { get; }
        string ProcessDescription { get; }
        bool IsActive { get; }
        int SourceConnectionTypeId { get; }
        // CONTINUE HERE
    }

    public interface IConnectionTypeRepository
    {
        IConnectionType[] GetList();
        IConnectionType GetById(int id);
    }

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