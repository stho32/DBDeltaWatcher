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
            
            Assert.Equal("MirroredId", derivedTable.Columns[0].ColumnName);
            Assert.Equal("SourceChecksum", derivedTable.Columns[1].ColumnName);
            Assert.Equal("Old_SomeString", derivedTable.Columns[2].ColumnName);
            Assert.Equal("Old_LongString", derivedTable.Columns[3].ColumnName);
            Assert.Equal("Old_NumberColumn", derivedTable.Columns[4].ColumnName);
            Assert.Equal("Old_DecimalColumn", derivedTable.Columns[5].ColumnName);
            Assert.Equal("Old_BooleanColumn", derivedTable.Columns[6].ColumnName);

            Assert.True(derivedTable.Columns[0].IsPrimaryKey);
            Assert.False(derivedTable.Columns[1].IsPrimaryKey);
            Assert.False(derivedTable.Columns[2].IsPrimaryKey);
            Assert.False(derivedTable.Columns[3].IsPrimaryKey);
            Assert.False(derivedTable.Columns[4].IsPrimaryKey);
            Assert.False(derivedTable.Columns[5].IsPrimaryKey);
            Assert.False(derivedTable.Columns[6].IsPrimaryKey);
        }
    }
}