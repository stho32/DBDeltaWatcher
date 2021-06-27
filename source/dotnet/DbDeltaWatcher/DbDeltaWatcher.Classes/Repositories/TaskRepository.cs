using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Classes.Converters;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Entities;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Enums;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes.Repositories
{
    public class TaskRepository : RepositoryBase<ITask>, ITaskRepository
    {
        public TaskRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        protected override string GetSelectSql(string additionalWhere = "")
        {
            var sql = @"
SELECT Id, 
       /* Process Information */
       ProcessName,
       ProcessDescription,
       IsExecutionExplicitlyRequested, 
       LastExecutionTime,
   
       /* Source Connection */
       SourceConnectionTypeId,
       SourceConnectionStringName,
   
       /* Source Table Description */
       SourceTableName,
       /* Mirror Table Description */
       MirrorTableName,
       
       /* Transformation Target Connection */
       TransformationTargetConnectionTypeId,
       TransformationTargetConnectionStringName,
   
       /* Transformation Description */
       OnDeletedRow,
       OnAddedRow,
       OnChangedRow,
       IsActive
  FROM DBDeltaWatcher_Task
 WHERE IsActive = 1;
 ";
            sql = AddAdditionalWhere(sql, additionalWhere);

            return sql;
        }

        protected override ITask CreateInstance(DataRow row)
        {
            return new Task(
                row["Id"].ToInt(),
                row["IsActive"].ToBool(),
                new ProcessInformation(
                    row["ProcessName"].ToString(),
                    row["ProcessDescription"].ToString(),
                    row["IsExecutionExplicitlyRequested"].ToBool(),
                    row["LastExecutionTime"].ToNullableDateTime()
                    ),
                new ConnectionDescription(
                     row["SourceConnectionTypeId"].ToInt().AsConnectionType(),
                     row["SourceConnectionStringName"].ToString()
                    ),
                new TableDescription(row["SourceTableName"].ToString()),
                new TableDescription(row["MirrorTableName"].ToString()),
                new ConnectionDescription(
                    row["TransformationTargetConnectionTypeId"].ToInt().AsConnectionType(),
                    row["TransformationTargetConnectionStringName"].ToString()),
                new TransformationDescription(
                        row["OnDeletedRow"].ToString(),
                        row["OnAddedRow"].ToString(),
                        row["OnChangedRow"].ToString()
                    )
            );
        }

        public ITask[] GetList()
        {
            return LoadData(GetSelectSql(), null, CreateInstance);
        }

        public ITask GetById(int id)
        {
            return LoadOneObject(GetSelectSql("Id = @Id"),
                new Dictionary<string, object>
                {
                    {"@Id", id}
                }, CreateInstance); 
        }
    }
}