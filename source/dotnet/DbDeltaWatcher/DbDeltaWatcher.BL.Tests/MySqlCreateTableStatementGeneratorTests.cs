using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
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
);", sql.Trim());
        }

        [Fact]
        public void can_add_code_for_a_primary_key_column()
        {
            var generator = new CreateTableStatementGenerator(new MySqlDialect());
            var columns = new List<ISimplifiedColumnSchema>();
            columns.Add(new SimplifiedColumnSchema(0, "Id", "INT", 0, 0, 0, true));
            var sql = generator.Generate("TestTable", columns.ToArray());
            Assert.Equal(@"CREATE TABLE TestTable (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT
);", sql.Trim());
        }

        [Fact]
        public void the_code_for_the_testtable_is_generated_correctly()
        {
            var columnList = new List<ISimplifiedColumnSchema>
            {
                new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true),
                new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false),
                new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false),
                new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false),
                new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false),
                new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false)
            };

            var generator = new CreateTableStatementGenerator(new MySqlDialect());
            var sql = generator.Generate("TestTable", columnList.ToArray());

            Assert.Equal(@"CREATE TABLE TestTable (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    SomeString VARCHAR(200),
    LongString MEDIUMTEXT,
    NumberColumn INT,
    DecimalColumn DECIMAL(15,4),
    BooleanColumn TINYINT
);", sql.Trim());

        }
    }
}