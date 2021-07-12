using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.SqlServerSupport;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using Xunit;

namespace DbDeltaWatcher.BL.Tests
{
    public class AlterTableStatementGeneratorTests
    {
        [Fact]
        public void can_create_sql_to_add_a_column()
        {
            var createSql = new AlterTableScriptGenerator(new SqlServerDialect());
            var sql = createSql.MigrationSql(
                new SimplifiedTableSchemaChanges(
                    "TargetTable",
                    new ISimplifiedTableSchemaChange[]
                    {
                        new SimplifiedTableSchemaChange(SimplifiedTableSchemaChangeEnum.AddColumn,
                            new SimplifiedColumnSchema(0, "Bob", "varchar", 200, 0,0,false))
                    })
                );
            
            Assert.Equal(@"ALTER TABLE TargetTable ADD Bob VARCHAR(200);", sql.Trim());
        }

        [Fact]
        public void can_create_sql_to_remove_a_column()
        {
            Assert.True(false);
        }

        [Fact]
        public void can_create_sql_to_alter_the_datatype_of_a_column()
        {
            Assert.True(false);
        }
    }
}