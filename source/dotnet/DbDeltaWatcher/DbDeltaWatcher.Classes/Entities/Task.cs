using System;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class Task : ITask
    {
        public int Id { get; }
        public string ProcessName { get; }
        public string ProcessDescription { get; }
        public bool IsActive { get; }
        public int SourceConnectionTypeId { get; }
        public string SourceConnectionStringName { get; }
        public string SourceName { get; }
        public string SourceSql { get; }
        public string SourceChecksumColumn { get; }
        public int DestinationConnectionTypeId { get; }
        public string DestinationConnectionStringName { get; }
        public string DestinationOnDeletedRow { get; }
        public string DestinationOnAddedRow { get; }
        public string DestinationOnChangedRow { get; }
        public string SourceMirrorTableName { get; }
        public bool IsSourceMirrorTableLocationInSource { get; }
        public bool IsExecutionExplicitlyRequested { get; }
        public DateTime? LastExecutionTime { get; }

        public Task(int id, string processName, string processDescription, bool isActive, int sourceConnectionTypeId, string sourceConnectionStringName, string sourceName, string sourceSql, string sourceChecksumColumn, int destinationConnectionTypeId, string destinationConnectionStringName, string destinationOnDeletedRow, string destinationOnAddedRow, string destinationOnChangedRow, string sourceMirrorTableName, bool isSourceMirrorTableLocationInSource, bool isExecutionExplicitlyRequested, DateTime? lastExecutionTime)
        {
            Id = id;
            ProcessName = processName;
            ProcessDescription = processDescription;
            IsActive = isActive;
            SourceConnectionTypeId = sourceConnectionTypeId;
            SourceConnectionStringName = sourceConnectionStringName;
            SourceName = sourceName;
            SourceSql = sourceSql;
            SourceChecksumColumn = sourceChecksumColumn;
            DestinationConnectionTypeId = destinationConnectionTypeId;
            DestinationConnectionStringName = destinationConnectionStringName;
            DestinationOnDeletedRow = destinationOnDeletedRow;
            DestinationOnAddedRow = destinationOnAddedRow;
            DestinationOnChangedRow = destinationOnChangedRow;
            SourceMirrorTableName = sourceMirrorTableName;
            IsSourceMirrorTableLocationInSource = isSourceMirrorTableLocationInSource;
            IsExecutionExplicitlyRequested = isExecutionExplicitlyRequested;
            LastExecutionTime = lastExecutionTime;
        }
    }
}