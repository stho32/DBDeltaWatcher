using System;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class ProcessInformation : IProcessInformation
    {
        public string ProcessName { get; }
        public string ProcessDescription { get; }
        public bool IsExecutionExplicitlyRequested { get; }
        public DateTime? LastExecutionTime { get; }

        public ProcessInformation(string processName, string processDescription, bool isExecutionExplicitlyRequested, DateTime? lastExecutionTime)
        {
            ProcessName = processName;
            ProcessDescription = processDescription;
            IsExecutionExplicitlyRequested = isExecutionExplicitlyRequested;
            LastExecutionTime = lastExecutionTime;
        }
    }
}