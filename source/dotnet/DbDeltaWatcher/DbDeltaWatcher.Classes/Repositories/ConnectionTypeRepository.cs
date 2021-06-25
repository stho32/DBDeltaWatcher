using System.Data;
using DbDeltaWatcher.Classes.Converters;
using DbDeltaWatcher.Classes.Entities;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Entities;
using DbDeltaWatcher.Interfaces.Repositories;

namespace DbDeltaWatcher.Classes.Repositories
{
    public class ConnectionTypeRepository : RepositoryBase<IConnectionType>, IConnectionTypeRepository
    {
        protected ConnectionTypeRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        public IConnectionType[] GetList()
        {
            throw new System.NotImplementedException();
        }

        public IConnectionType GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetSelectSql(string additionalWhere = "")
        {
            var sql = @"
SELECT Id,
       [Name],
       TechnicalIdentifier,
       HasConnectionStringName,
       HasSQL,
       IsActive
  FROM DBDeltaWatcher_ConnectionType
 WHERE IsActive = 1
";
            sql = AddAdditionalWhere(sql, additionalWhere);

            return sql;
        }

        protected override ConnectionType CreateInstance(DataRow row)
        {
            return new ConnectionType(
                row["Id"].ToInt(),
                row["Name"].ToString(),
                row["TechnicalIdentifier"].ToString(),
                row["HasConnectionStringName"].ToBool(),
                row["HasSQL"].ToBool(),
                row["IsActive"].ToBool()
            );
        }
    }
}