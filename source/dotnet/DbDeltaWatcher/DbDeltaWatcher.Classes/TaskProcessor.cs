using System;
using System.Threading.Tasks;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Classes.ExtensionMethods;

namespace DbDeltaWatcher.Classes
{
    public class TaskProcessor : ITaskProcessor
    {
        private readonly ITask _task;
        private readonly IDatabaseSupport _databaseSupport;

        public TaskProcessor(
            ITask task,
            IDatabaseSupport databaseSupport)
        {
            _task = task;
            _databaseSupport = databaseSupport;
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
            // Ensure that the source does exist...
            
            var sourceSchemaProvider = _databaseSupport.GetSchemaProvider(_task.SourceConnection);

            if (!sourceSchemaProvider.TableExists(_task.SourceTable.TableName))
            {
                throw new Exception($"Source table {_task.SourceTable.TableName} does not exist :(.");
            }

            var sourceTableSchema = sourceSchemaProvider.GetSimplifiedTableSchema(_task.SourceTable.TableName);
            sourceTableSchema.DeriveMirrorSchema(_task.MirrorTable.TableName, _databaseSupport.GetSqlDialect(_task.SourceConnection));
            
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
