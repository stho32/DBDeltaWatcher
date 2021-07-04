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
    }
}