using DbDeltaWatcher.Classes.Database.CommonSqlSupport;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlDialect : SqlDialectBase
    {
        public MySqlDialect() : base(
            new IColumnDefinitionGenerator[]
            {
                new MySqlPrimaryKeyColumnDefinitionGenerator(),
                new VarcharColumnDefinitionGenerator(),
                new SimplyTheDatatypeColumnDefinitionGenerator("MEDIUMTEXT"),
                new SimplyTheDatatypeColumnDefinitionGenerator("INT"),
                new SimplyTheDatatypeColumnDefinitionGenerator("DATETIME"),
                new DecimalColumnDefinitionGenerator(),
                new SimplyTheDatatypeColumnDefinitionGenerator("TINYINT")
            })
        {
        }
    }
}