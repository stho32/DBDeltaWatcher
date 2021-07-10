using System.Collections.Generic;
using System.Data;

namespace DbDeltaWatcher.Interfaces.Database.DatabaseConnections
{
    /// <summary>
    /// A connection to a database
    /// </summary>
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Simply execute the sql that is added
        /// </summary>
        /// <param name="sql">the sql</param>
        /// <param name="parameters">the parameters contained in the sql</param>
        public void ExecuteSql(string sql, Dictionary<string, object> parameters = null);
        
        /// <summary>
        /// Request data and put it into a datatable
        /// </summary>
        /// <param name="sql">the sql</param>
        /// <param name="parameters">the necessary parameters</param>
        /// <returns></returns>
        public DataTable LoadDataTable(string sql, Dictionary<string, object> parameters = null);
    }
}