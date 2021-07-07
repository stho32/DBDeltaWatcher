using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbDeltaWatcher.Classes.ExtensionMethods;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public abstract class SchemaProviderBase : ISchemaProvider
    {
        private readonly IDatabaseConnection _connection;

        protected SchemaProviderBase(IDatabaseConnection connection)
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
            var primaryKeys = GetPrimaryKey(tableName);
            
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
                columns.Add(CreateInstance(row, primaryKeys.Any(x=> x.ToLower() == row["COLUMN_NAME"].ToString().ToLower()))); 
            }

            return new SimplifiedTableSchema(tableName, columns.ToArray());
        }

        private string[] GetPrimaryKey(string tableName)
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

        private SimplifiedColumnSchema CreateInstance(DataRow row, bool isPrimaryKey)
        {
            return new SimplifiedColumnSchema(
                row["ORDINAL_POSITION"].ToInt(),
                row["COLUMN_NAME"].ToString(),
                row["DATA_TYPE"].ToString(),
                row["CHARACTER_MAXIMUM_LENGTH"].ToLong(),
                row["NUMERIC_PRECISION"].ToInt(),
                row["NUMERIC_SCALE"].ToInt(), 
                isPrimaryKey
            );
        }
    }
}