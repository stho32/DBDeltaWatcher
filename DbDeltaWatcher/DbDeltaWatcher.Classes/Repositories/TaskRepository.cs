using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Classes.Converters;
using DbDeltaWatcher.Classes.Entities;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes.Repositories
{
    public class TaskRepository : RepositoryBase, ITaskRepository
    {
        public TaskRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        private string GetSelectSql(string additionalWhere = "")
        {
            var sql = @"
SELECT Id, 
       ProcessName, 
       ProcessDescription,
       IsActive,
       
       SourceConnectionTypeId,
       SourceConnectionStringName,
       SourceName,
       SourceSQL,
       SourceChecksumColumn,

       DestinationConnectionTypeId,
       DestinationConnectionStringName,
       DestinationOnDeletedRow,
       DestinationOnAddedRow,
       DestinationOnChangedRow,

       SourceMirrorTableName,
       IsSourceMirrorTableLocationInSource,

       IsExecutionExplicitlyRequested,
       LastExecutionTime
  FROM DBDeltaWatcher_Task
 WHERE IsActive = 1;
";
            if (!string.IsNullOrWhiteSpace(additionalWhere))
            {
                sql = sql + " AND (" + additionalWhere + ")";
            }

            return sql;
        }

        private ITask CreateInstance(DataRow row)
        {
            return new Task(
                row["Id"].ToInt(),
                row["ProcessName"].ToString(),
                row["processDescription"].ToString(),
                row["IsActive"].ToBool(),
                row["SourceConnectionTypeId"].ToInt(),
                row["SourceConnectionStringName"].ToString(),
                row["SourceName"].ToString(),
                row["SourceSql"].ToString(),
                row["SourceChecksumColumn"].ToString(),
                row["DestinationConnectionTypeId"].ToInt(),
                row["DestinationConnectionStringName"].ToString(),
                row["DestinationOnDeletedRow"].ToString(),
                row["DestinationOnAddedRow"].ToString(),
                row["DestinationOnChangedRow"].ToString(),
                row["SourceMirrorTableName"].ToString(),
                row["IsSourceMirrorTableLocationInSource"].ToBool(),
                row["IsExecutionExplicitlyRequested"].ToBool(),
                row["LastExecutionTime"].ToNullableDateTime()
            );
        }

        public ITask[] GetList()
        {
            return LoadData<ITask>(GetSelectSql(), null, CreateInstance);
        }

        public ITask GetById(int id)
        {
            return LoadOneObject<ITask>(GetSelectSql("Id = @Id"),
                new Dictionary<string, object>
                {
                    {"@Id", id}
                }, CreateInstance); 
        }
    }
}