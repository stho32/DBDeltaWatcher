using System;
using System.Collections.Generic;
using System.Data;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Repositories
{
    public class RepositoryBase
    {
        private readonly IDatabaseConnection _connection;

        protected RepositoryBase(IDatabaseConnection connection)
        {
            _connection = connection;
        }
        
        /**
         * Convert the result from an sql request
         */
        protected T[] LoadData<T>(string sql, Dictionary<string,object> parameters, Func<DataRow,T> createInstance)
        {
            var data = _connection.LoadDataTable(sql, parameters);
            var result = new List<T>();

            foreach (DataRow row in data.Rows)
            {
                result.Add(createInstance(row));
            }

            return result.ToArray();
        }

        protected T LoadOneObject<T>(string sql, Dictionary<string,object> parameters, Func<DataRow, T> createInstance)
        {
            var data = LoadData<T>(sql, parameters, createInstance);
            
            if (data.Length == 1)
            {
                return data[0];
            }

            return default(T);
        }
    }
}