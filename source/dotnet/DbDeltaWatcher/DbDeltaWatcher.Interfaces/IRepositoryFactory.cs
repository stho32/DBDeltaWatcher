using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Interfaces
{
    public interface IRepositoryFactory
    {
        ITaskRepository TaskRepository { get; }

        ITaskProcessor CreateTaskProcessor(ITask task);
    }

    public interface ITaskProcessor
    {
        void Execute();
    }

    public class TaskProcessor : ITaskProcessor
    {
        public void Execute()
        {
            EnsureValidMirrorExists();
            //var changedRows = GetChangedRows();
            //ProcessChangedRows(changedRows);
            //UpdateTaskStatistics();
        }

        private void UpdateTaskStatistics()
        {
            // ...
        }

        private void ProcessChangedRows(object changedRows)
        {
            // Handle added rows + commit changes to mirror
            // Handle deleted rows + commit changes to mirror
            // Handle changed rows + commit changes to mirror
        }

        private void EnsureValidMirrorExists()
        {
            //ITableSchema (source)
            //DeriveMirrorSchema
            //CreateMirrorTableSchema
            //UpdateMirrorTableSchema
        }
    }
}