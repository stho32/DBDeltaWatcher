using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using Xunit;

namespace DbDeltaWatcher.BL.Tests
{
    public class MySqlCreateTableStatementGeneratorTests
    {
        [Fact]
        public void can_generate_an_empty_create_table_statement()
        {
            var generator = new CreateTableStatementGenerator(new MySqlDialect());
            var sql = generator.Generate("TestTable", System.Array.Empty<ISimplifiedColumnSchema>());

            Assert.Equal(@"CREATE TABLE TestTable (
)", sql.Trim());
        }

        [Fact]
        public void can_add_code_for_a_primary_key_column()
        {
            var generator = new CreateTableStatementGenerator(new MySqlDialect());
            var columns = new List<ISimplifiedColumnSchema>();
            columns.Add(new SimplifiedColumnSchema(0, "Id", "INT", 0, 0, 0, true));
            var sql = generator.Generate("TestTable", columns.ToArray());
            Assert.Equal(@"CREATE TABLE TestTable (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
)", sql.Trim());
        }
    }
}