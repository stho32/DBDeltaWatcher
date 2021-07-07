using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerSchemaProvider : SchemaProviderBase
    {
        public SqlServerSchemaProvider(IDatabaseConnection connection) : base(connection)
        {
        }

        protected override string[] GetPrimaryKey(string tableName)
        {
            var sql = @"
SELECT COLUMN_NAME
  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
  JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON tc.CONSTRAINT_NAME = ccu.Constraint_name
 WHERE tc.TABLE_NAME = @tableName 
   AND tc.CONSTRAINT_TYPE = 'Primary Key'";
            
            var result = _connection.LoadDataTable(sql, new Dictionary<string, object>
            {
                {"@tableName", tableName}
            });

            var primaryKeys = new List<string>();

            foreach (DataRow row in result.Rows)
            {
                primaryKeys.Add(row["COLUMN_NAME"].ToString());
            }

            return primaryKeys.ToArray();
        }
    }
}