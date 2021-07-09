using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Classes.ExtensionMethods;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using Xunit;

namespace DbDeltaWatcher.BL.Tests
{
    public class DeriveMirrorTests
    {
        [Fact]
        public void the_testtable_is_mirrored_correctly()
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
            var table = new SimplifiedTableSchema("SomeTable", columnList.ToArray());

            var derivedTable = table.DeriveMirrorSchema("MirrorOfSomeTable", new MySqlDialect());
            
            Assert.Equal("MirrorOfSomeTable", derivedTable.TableName);
        }
    }
}