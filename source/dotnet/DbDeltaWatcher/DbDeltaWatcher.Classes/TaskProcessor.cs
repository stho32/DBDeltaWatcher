using System;
using System.Threading.Tasks;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes
{
    public class TaskProcessor : ITaskProcessor
    {
        private readonly ITask _task;
        private readonly IDatabaseConnection _sourceConnection;

        public TaskProcessor(
            ITask task,
            IDatabaseConnection sourceConnection)
        {
            _task = task;
            _sourceConnection = sourceConnection;
        }
        
        public void Execute()
        {
            EnsureValidMirrorExistsAndIsValid();
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

        private void EnsureValidMirrorExistsAndIsValid()
        {
            var connection = _task.SourceConnection;
            
            // // Ensure that the source does exist...
            // var sourceSchemaProvider = _factory.GetSchemaProviderFor(_task.SourceConnection);
            // if (!sourceSchemaProvider.TableExists(_task.SourceTable.TableName))
            //     throw new Exception($"Source table {_task.SourceTable.TableName} does not exist :(.");
            //
            // var sourceTableSchema = sourceSchemaProvider.GetTableSchema(_task.SourceTable.TableName);
            // var derivedMirrorTableSchema = sourceSchema.DeriveMirror();
            //
            // // If there is no mirror table here...
            // if (!sourceSchemaProvider.TableExists(_task.MirrorTable.TableName))
            // {
            //     var sql = derivedMirrorTableSchema.ToSqlCreateTable();
            //     connection.Execute(sql);
            // }
            // else
            // {
            //     var existingMirrorTableSchema = sourceSchemaProvider.GetTableSchema(_task.MirrorTable.TableName);
            //     var differences = derivedMirrorTableSchema.DifferenceTo(existingMirrorTableSchema);
            //     if (differences.Length > 0)
            //     {
            //         var sql = differences.ToSqlAlterTable();
            //         connection.Execute(sql);
            //     }
            // }
        }
    }
}