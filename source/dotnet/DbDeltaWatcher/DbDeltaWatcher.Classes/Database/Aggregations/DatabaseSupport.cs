using System.Linq;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.DatabaseConnections;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.Aggregations
{
    public class DatabaseSupport : IDatabaseSupport
    {
        private readonly IDatabaseSupport[] _databaseSupports;

        public DatabaseSupport(
            IDatabaseSupport[] databaseSupports)
        {
            _databaseSupports = databaseSupports;
        }

        public bool IsSupportFor(IConnectionDescription connectionDescription)
        {
            return _databaseSupports.Any(
                    support => support.IsSupportFor(connectionDescription)
                );
        }

        public ISchemaProvider GetSchemaProvider(IConnectionDescription connectionDescription)
        {
            var support = GetCompatibleDatabaseSupport(connectionDescription);
            return support?.GetSchemaProvider(connectionDescription);
        }

        private IDatabaseSupport GetCompatibleDatabaseSupport(IConnectionDescription connectionDescription)
        {
            var support = _databaseSupports.FirstOrDefault(x=>x.IsSupportFor(connectionDescription));
            return support;
        }

        public IDatabaseConnection GetDatabaseConnection(IConnectionDescription connectionDescription)
        {
            var support = GetCompatibleDatabaseSupport(connectionDescription);
            return support?.GetDatabaseConnection(connectionDescription);
        }

        public ISqlDialect GetSqlDialect(IConnectionDescription connectionDescription)
        {
            var support = GetCompatibleDatabaseSupport(connectionDescription);
            return support?.GetSqlDialect(connectionDescription);
        }
    }
}