using System.Collections.Generic;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.ExtensionMethods;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;
using Xunit;

namespace DbDeltaWatcher.BL.Tests
{
    public class SchemaDifferenceDetectionTests
    {
        [Fact]
        public void when_source_and_target_are_the_same_no_changes_are_found()
        {
            var source = new SimplifiedTableSchema("Table",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                    new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false)
                });
            
            var target = new SimplifiedTableSchema("AnotherTable",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                    new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false)
                });

            var difference = source.PrepareMigrationTo(target);
            Assert.Empty(difference.Changes);
        }

        [Fact]
        public void when_source_has_more_columns_then_add_is_proposed()
        {
            var source = new SimplifiedTableSchema("Table",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                    new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false),
                    new SimplifiedColumnSchema(6, "AColumnMore", "varchar", 200, 0, 0, false), 
                });
            
            var target = new SimplifiedTableSchema("AnotherTable",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                    new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false)
                });

            var difference = source.PrepareMigrationTo(target);
            Assert.Single(difference.Changes);
            Assert.Equal(SimplifiedTableSchemaChangeEnum.AddColumn, difference.Changes[0].TypeOfChange);
            Assert.Equal("AColumnMore", difference.Changes[0].ColumnSchema.ColumnName);
        }

        [Fact]
        public void when_the_datatype_of_a_column_has_changed_an_alteration_is_proposed()
        {
            var source = new SimplifiedTableSchema("Table",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 400, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "bigint", 0, 10, 0, false), 
                });
            
            var target = new SimplifiedTableSchema("AnotherTable",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                });

            var difference = source.PrepareMigrationTo(target);
            Assert.Equal(2, difference.Changes.Length);
            
            Assert.Equal(SimplifiedTableSchemaChangeEnum.ChangeDataType, difference.Changes[0].TypeOfChange);
            Assert.Equal("varchar", difference.Changes[0].ColumnSchema.DataType);
            Assert.Equal(400, difference.Changes[0].ColumnSchema.CharacterMaximumLength);

            Assert.Equal(SimplifiedTableSchemaChangeEnum.ChangeDataType, difference.Changes[1].TypeOfChange);
            Assert.Equal("bigint", difference.Changes[1].ColumnSchema.DataType);
        }

        [Fact]
        public void when_source_has_less_columns_then_drop_is_proposed()
        {
            var source = new SimplifiedTableSchema("Table",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(4, "BooleanColumn", "tinyint", 0, 3, 0, false)
                });
            
            var target = new SimplifiedTableSchema("AnotherTable",
                new ISimplifiedColumnSchema[]
                {
                    new SimplifiedColumnSchema(0, "Id", "int", 0, 10, 0, true), 
                    new SimplifiedColumnSchema(1, "SomeString", "varchar", 200, 0, 0, false), 
                    new SimplifiedColumnSchema(2, "LongString", "mediumtext", 16777215, 0, 0, false), 
                    new SimplifiedColumnSchema(3, "NumberColumn", "int", 0, 10, 0, false), 
                    new SimplifiedColumnSchema(4, "DecimalColumn", "decimal", 0, 15, 4, false), 
                    new SimplifiedColumnSchema(5, "BooleanColumn", "tinyint", 0, 3, 0, false)
                });

            var difference = source.PrepareMigrationTo(target);
            Assert.Single(difference.Changes);
            Assert.Equal(SimplifiedTableSchemaChangeEnum.RemoveColumn, difference.Changes[0].TypeOfChange);
            Assert.Equal("NumberColumn", difference.Changes[0].ColumnSchema.ColumnName);
        }
    }
}