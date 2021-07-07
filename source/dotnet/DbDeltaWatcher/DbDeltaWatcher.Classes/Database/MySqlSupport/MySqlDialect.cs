using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher.Classes.Database.MySqlSupport
{
    public class MySqlDialect : SqlDialectBase
    {
        public MySqlDialect() : base(
            new IColumnDefinitionGenerator[]
            {
                new MySqlPrimaryKeyColumnDefinitionGenerator(),
                new MySqlVarcharColumnDefinitionGenerator(),
                new MySqlMediumtextColumnDefinitionGenerator(),
                new MySqlIntColumnDefinitionGenerator(),
                new MySqlDecimalColumnDefinitionGenerator(),
                new MySqlTinyIntColumnDefinitionGenerator()
            })
        {
        }
    }
}