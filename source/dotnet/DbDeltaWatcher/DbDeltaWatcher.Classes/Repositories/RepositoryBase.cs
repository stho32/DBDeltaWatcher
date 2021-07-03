using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;

namespace DbDeltaWatcher.Classes.Repositories
{
    public abstract class RepositoryBase<T>
    {
        private readonly IDatabaseConnection _connection;

        protected RepositoryBase(IDatabaseConnection connection)
        {
            _connection = connection;
        }
        
        /**
         * Convert the result from an sql request
         */
        protected T[] LoadData(string sql, Dictionary<string,object> parameters, Func<DataRow,T> createInstance)
        {
            var data = _connection.LoadDataTable(sql, parameters);
            var result = new List<T>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(createInstance(row));
            }

            return result.ToArray();
        }

        protected T LoadOneObject(string sql, Dictionary<string,object> parameters, Func<DataRow, T> createInstance)
        {
            var data = LoadData(sql, parameters, createInstance);
            
            if (data.Length == 1)
            {
                return data[0];
            }

            return default(T);
        }
        
        protected abstract string GetSelectSql(string additionalWhere = "");
        protected abstract T CreateInstance(DataRow row);

        protected static string AddAdditionalWhere(string sql, string additionalWhere, string @operator = "AND")
        {
            if (!string.IsNullOrWhiteSpace(additionalWhere))
            {
                sql = sql + " " + @operator + " (" + additionalWhere + ")";
            }

            return sql;
        }
    }
}