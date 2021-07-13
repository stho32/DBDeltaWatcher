using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbDeltaWatcher.Classes.Database;
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
            var changedRows = GetChangedRows();
            //ProcessChangedRows(changedRows);
            //UpdateTaskStatistics();
        }

        private IChange GetChangedRows()
        {
            throw new NotImplementedException();
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
            var sourceSqlDialect = _databaseSupport.GetSqlDialect(_task.SourceConnection);
            var derivedMirrorSchema = sourceTableSchema.DeriveMirrorSchema(_task.MirrorTable.TableName, sourceSqlDialect);
            var sourceConnection = _databaseSupport.GetDatabaseConnection(_task.SourceConnection);
            
            // If there is no mirror table here...
            if (!sourceSchemaProvider.TableExists(_task.MirrorTable.TableName))
            {
                var createSql = new CreateTableStatementGenerator(sourceSqlDialect);
                var sql = createSql.Generate(derivedMirrorSchema.TableName, derivedMirrorSchema.Columns);
                sourceConnection.ExecuteSql(sql);
            }
            else
            {
                // If there is already a mirror table, we have to make sure, that it
                // fits the structure of the source table. Remember: source tables can change
                // during their lifetime. Thus we need to make sure, that the mirror updates accordingly.
                
                var existingMirrorTableSchema = sourceSchemaProvider.GetSimplifiedTableSchema(_task.MirrorTable.TableName);
                var differences = existingMirrorTableSchema.PrepareMigrationTo(derivedMirrorSchema);
                if (differences.Changes.Length > 0)
                {
                    var createSql = new AlterTableScriptGenerator(sourceSqlDialect);
                    var sql = createSql.MigrationSql(differences);
                    sourceConnection.ExecuteSql(sql);
                }
            }
        }
    }
}
