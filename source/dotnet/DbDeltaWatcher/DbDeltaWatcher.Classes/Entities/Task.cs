using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class Task : ITask
    {
        public int Id { get; }
        public bool IsActive { get; }
        public IProcessInformation ProcessInformation { get; }
        public IConnectionDescription SourceConnection { get; }
        public ITableDescription SourceTable { get; }
        public ITableDescription MirrorTable { get; }
        public IConnectionDescription TransformationTargetConnection { get; }
        public ITransformationDescription TransformationDescription { get; }

        public Task(
            int id, 
            bool isActive, 
            IProcessInformation processInformation,
            IConnectionDescription sourceConnection,
            ITableDescription sourceTable,
            ITableDescription mirrorTable,
            IConnectionDescription transformationTargetConnection,
            ITransformationDescription transformationDescription)
        {
            Id = id;
            IsActive = isActive;
            ProcessInformation = processInformation;
            SourceConnection = sourceConnection;
            SourceTable = sourceTable;
            MirrorTable = mirrorTable;
            TransformationTargetConnection = transformationTargetConnection;
            TransformationDescription = transformationDescription;
        }
    }
}