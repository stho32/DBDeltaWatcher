using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlSchemaProvider : SchemaProviderBase
    {
        public MySqlSchemaProvider(IDatabaseConnection connection) : base(connection)
        {
        }

        protected override string[] GetPrimaryKey(string tableName)
        {
            var sql = @"
SELECT COLUMN_NAME
  FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = @tableName 
   AND COLUMN_KEY = 'PRI'
";
            var data = _connection.LoadDataTable(sql, new Dictionary<string, object>
            {
                {"@tableName", tableName}
            });

            var result = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                result.Add(row["COLUMN_NAME"].ToString());
            }
            return result.ToArray();
        }
    }
}