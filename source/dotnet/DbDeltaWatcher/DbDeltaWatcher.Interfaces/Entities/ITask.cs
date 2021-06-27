using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Interfaces.Entities
{
    public interface ITask
    {
        int Id { get; }
        bool IsActive { get; }
        
        IProcessInformation ProcessInformation { get; }
        
        IConnectionDescription SourceConnection { get; }
        ITableDescription SourceTable { get; }
        ITableDescription MirrorTable { get; }
        
        IConnectionDescription TransformationTargetConnection { get; }
        ITransformationDescription TransformationDescription { get; }
    }
}