using System;

namespace DbDeltaWatcher.Interfaces.Repositories
{
    public interface ITask
    {
        int Id { get; }
        string ProcessName { get; }
        string ProcessDescription { get; }
        bool IsActive { get; }
        int SourceConnectionTypeId { get; }
        string SourceConnectionStringName { get; }
        string SourceName { get; }
        string SourceSql { get; }
        string SourceChecksumColumn { get; }
        int DestinationConnectionTypeId { get; }
        string DestinationConnectionStringName { get; }
        string DestinationOnDeletedRow { get; }
        string DestinationOnAddedRow { get; }
        string DestinationOnChangedRow { get; }
        string SourceMirrorTableName { get; }
        bool IsSourceMirrorTableLocationInSource { get; }
        bool IsExecutionExplicitlyRequested { get; }
        DateTime? LastExecutionTime { get; }
    }
}