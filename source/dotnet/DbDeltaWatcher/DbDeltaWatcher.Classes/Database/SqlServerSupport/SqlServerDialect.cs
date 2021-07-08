using DbDeltaWatcher.Classes.Database.CommonSqlSupport;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database.SqlServerSupport
{
    public class SqlServerDialect : SqlDialectBase
    {
        public SqlServerDialect() : base(
            new IColumnDefinitionGenerator[]
            {
                new SqlServerPrimaryKeyColumnDefinitionGenerator(),
                new VarcharColumnDefinitionGenerator(),
                new VarcharMaxColumnDefinitionGenerator(),
                new IntColumnDefinitionGenerator(),
                new DecimalColumnDefinitionGenerator(),
                new SqlServerBitColumnDefinitionGenerator()
            })
        {
        }
    }
}