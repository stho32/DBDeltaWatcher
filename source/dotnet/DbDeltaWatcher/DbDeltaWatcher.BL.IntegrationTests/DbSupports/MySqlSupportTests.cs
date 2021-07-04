using System.Collections.Generic;
using Xunit;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.BL.IntegrationTests.DbSupports
{
    public class MySqlSupportTests
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
            Assert.Equal(0, schema.Columns[0].NumericPrecision);
            Assert.Equal(0, schema.Columns[0].NumericScale);
            
            Assert.Equal("SomeString", schema.Columns[1].ColumnName);
            Assert.Equal("varchar", schema.Columns[1].DataType);
            Assert.Equal(200, schema.Columns[1].CharacterMaximumLength);
            Assert.Equal(0, schema.Columns[1].NumericPrecision);
            Assert.Equal(0, schema.Columns[1].NumericScale);

            // Hier weiter .. ! 
            Assert.True(false);
        }
    }
}