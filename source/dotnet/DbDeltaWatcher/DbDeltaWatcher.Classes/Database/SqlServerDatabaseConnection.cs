using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database
{
    /**
     * Connect us to a MS SQL Server based database
     */
    public class SqlServerDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;

        public SqlServerDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void ExecuteSql(string sql, Dictionary<string, object> parameters = null)
        {
            PerformDatabaseOperation(sql, parameters, (sqlCommand) =>
            {
                sqlCommand.ExecuteNonQuery();
            });
        }

        public DataTable LoadDataTable(string sql, Dictionary<string, object> parameters)
        {
            var datatable = new DataTable();

            PerformDatabaseOperation(sql, parameters, (sqlCommand) =>
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    adapter.Fill(datatable);
                }
            });

            return datatable;
        }

        private void PerformDatabaseOperation(
            string sql, 
            Dictionary<string, object> parameters, 
            Action<SqlCommand> operation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommand = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var (key, value) in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(key, value);
                        }
                    }

                    operation(sqlCommand);
                }
            }
        }
    }
}