using System.Collections.Generic;
using DbDeltaWatcher.BL.IntegrationTests.Interfaces;
using Xunit;
using DbDeltaWatcher.Classes.Database.MySqlSupport;

namespace DbDeltaWatcher.BL.IntegrationTests.DbSupports
{
    public class MySqlSupportTests : IDatabaseSupportTests
    {
        [Fact]
        public void a_connection_can_be_established()
        {
            if (!IntegrationTestConfiguration.Available())
                return;

            var support = new MySqlServerDatabaseSupport(
                IntegrationTestConfiguration.GetMySqlConnectionStringProvider);

            var connectionDescription = IntegrationTestConfiguration.GetMySqlConnectionDescription();
            Assert.True(support.IsSupportFor(connectionDescription));

            var connection = support.GetDatabaseConnection(connectionDescription);
            var result = connection.LoadDataTable(@"SELECT 1", new Dictionary<string, object>());
            
            Assert.Equal(1, result.Rows.Count);
        }

        [Fact]
        public void the_schemaprovider_does_its_job()
        {
            if (!IntegrationTestConfiguration.Available())
                return;

            var support = new MySqlServerDatabaseSupport(
                IntegrationTestConfiguration.GetMySqlConnectionStringProvider);

            var connectionDescription = IntegrationTestConfiguration.GetMySqlConnectionDescription();

            var provider = support.GetSchemaProvider(connectionDescription);
            
            Assert.True(provider.TableExists("user"));
            Assert.False(provider.TableExists("NotExisting"));
        }

        [Fact]
        public void the_schemaprovider_can_grab_a_table_structure()
        {
            if (!IntegrationTestConfiguration.Available())
                return;

            var support = new MySqlServerDatabaseSupport(
                IntegrationTestConfiguration.GetMySqlConnectionStringProvider);

            var connectionDescription = IntegrationTestConfiguration.GetMySqlConnectionDescription();

            var provider = support.GetSchemaProvider(connectionDescription);
            var schema = provider.GetSimplifiedTableSchema("TestTable");
            
            Assert.Equal("TestTable", schema.TableName);
            
            Assert.Equal("Id", schema.Columns[0].ColumnName);
            Assert.Equal("int", schema.Columns[0].DataType);
            Assert.Equal(0, schema.Columns[0].CharacterMaximumLength);
            Assert.Equal(10, schema.Columns[0].NumericPrecision);
            Assert.Equal(0, schema.Columns[0].NumericScale);
            
            Assert.Equal("SomeString", schema.Columns[1].ColumnName);
            Assert.Equal("varchar", schema.Columns[1].DataType);
            Assert.Equal(200, schema.Columns[1].CharacterMaximumLength);
            Assert.Equal(0, schema.Columns[1].NumericPrecision);
            Assert.Equal(0, schema.Columns[1].NumericScale);

            Assert.Equal("LongString", schema.Columns[2].ColumnName);
            Assert.Equal("mediumtext", schema.Columns[2].DataType);
            Assert.Equal(16777215, schema.Columns[2].CharacterMaximumLength);
            Assert.Equal(0, schema.Columns[2].NumericPrecision);
            Assert.Equal(0, schema.Columns[2].NumericScale);
            
            Assert.Equal("NumberColumn", schema.Columns[3].ColumnName);
            Assert.Equal("int", schema.Columns[3].DataType);
            Assert.Equal(0, schema.Columns[3].CharacterMaximumLength);
            Assert.Equal(10, schema.Columns[3].NumericPrecision);
            Assert.Equal(0, schema.Columns[3].NumericScale);

            Assert.Equal("DecimalColumn", schema.Columns[4].ColumnName);
            Assert.Equal("decimal", schema.Columns[4].DataType);
            Assert.Equal(0, schema.Columns[4].CharacterMaximumLength);
            Assert.Equal(15, schema.Columns[4].NumericPrecision);
            Assert.Equal(4, schema.Columns[4].NumericScale);

            Assert.Equal("BooleanColumn", schema.Columns[5].ColumnName);
            Assert.Equal("tinyint", schema.Columns[5].DataType);
            Assert.Equal(0, schema.Columns[5].CharacterMaximumLength);
            Assert.Equal(3, schema.Columns[5].NumericPrecision);
            Assert.Equal(0, schema.Columns[5].NumericScale);
        }
    }
}