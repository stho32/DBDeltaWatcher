using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Classes.ExtensionMethods;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerSchemaProvider : ISchemaProvider
    {
        private readonly IDatabaseConnection _connection;

        public SqlServerSchemaProvider(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public bool TableExists(string tableName)
        {
            var sql = @"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tableName";

            var result = _connection.LoadDataTable(sql, new Dictionary<string, object>
            {
                {"@tableName", tableName}
            });

            return result.Rows.Count > 0;
        }

        public ISimplifiedTableSchema GetSimplifiedTableSchema(string tableName)
        {
            var sql = @"
SELECT ORDINAL_POSITION, 
       COLUMN_NAME, 
       DATA_TYPE, 
       CHARACTER_MAXIMUM_LENGTH, 
       NUMERIC_PRECISION, 
       NUMERIC_SCALE 
  FROM INFORMATION_SCHEMA.COLUMNS 
 WHERE TABLE_NAME = @tableName
 ORDER BY ORDINAL_POSITION";

            var result = _connection.LoadDataTable(sql, new Dictionary<string, object>
            {
                {"@tableName", tableName}
            });

            var columns = new List<SimplifiedColumnSchema>();

            foreach (DataRow row in result.Rows)
            {
                columns.Add(CreateInstance(row)); 
            }

            return new SimplifiedTableSchema(tableName, columns.ToArray());
        }

        private SimplifiedColumnSchema CreateInstance(DataRow row)
        {
            return new SimplifiedColumnSchema(
                row["ORDINAL_POSITION"].ToInt(),
                row["COLUMN_NAME"].ToString(),
                row["DATA_TYPE"].ToString(),
                row["CHARACTER_MAXIMUM_LENGTH"].ToLong(),
                row["NUMERIC_PRECISION"].ToInt(),
                row["NUMERIC_SCALE"].ToInt()
            );
        }
    }
}