using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Interfaces.Database;
using MySql.Data.MySqlClient;

namespace DbDeltaWatcher.Classes.Database
{
    public class MySqlServerDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;

        public MySqlServerDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void ExecuteSql(string sql, Dictionary<string, object> parameters)
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
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand))
                {
                    adapter.Fill(datatable);
                }
            });

            return datatable;
        }

        private void PerformDatabaseOperation(string sql, Dictionary<string, object> parameters, Action<MySqlCommand> operation)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommand = new MySqlCommand(sql, connection))
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