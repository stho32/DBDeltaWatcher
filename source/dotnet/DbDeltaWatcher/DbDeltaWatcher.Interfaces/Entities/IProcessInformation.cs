using System;

namespace DbDeltaWatcher.Interfaces.Entities
{
    public interface IProcessInformation
    {
        string ProcessName { get; }
        string ProcessDescription { get; }
        bool IsExecutionExplicitlyRequested { get; }
        DateTime? LastExecutionTime { get; }
    }
}