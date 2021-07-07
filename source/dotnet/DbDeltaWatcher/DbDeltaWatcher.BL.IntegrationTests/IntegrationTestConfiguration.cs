using System;
using System.IO;
using System.Text.Json;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Enums;

namespace DbDeltaWatcher.BL.IntegrationTests
{
    public static class IntegrationTestConfiguration
    {
        private static readonly IntegrationTestConfigurationPoco Configuration = null;
        
        static IntegrationTestConfiguration()
        {
            Configuration = new IntegrationTestConfigurationPoco(false);

            if (!File.Exists(FileName))
            {
                File.WriteAllText(FileName,
                    JsonSerializer.Serialize(Configuration));
            }

            Configuration = 
                JsonSerializer.Deserialize<IntegrationTestConfigurationPoco>(
                File.ReadAllText(FileName));
        }
        
        private static string FileName => Path.Join(AppContext.BaseDirectory, "../../../../IntegrationTestConfiguration.json");

        public static IConnectionStringProvider GetMySqlConnectionStringProvider
        {
            get
            {
                return new ConnectionStringProvider(
                    new IConnectionStringProvider[]
                    {
                        new OneConnectionStringProvider(GetMySqlConnectionDescription(), new ConnectionString(Configuration.MySqlTestConnectionString))
                    });
            }
        }

        public static IConnectionStringProvider GetSqlServerConnectionStringProvider
        {
            get
            {
                return new ConnectionStringProvider(
                    new IConnectionStringProvider[]
                    {
                        new OneConnectionStringProvider(
                            GetSqlServerConnectionDescription(), 
                            new ConnectionString(Configuration.SqlServerTestConnectionString))
                    }
                );
            }
        }

        public static IConnectionDescription GetSqlServerConnectionDescription()
        {
            return new ConnectionDescription(ConnectionTypeEnum.SqlServer, "SqlServerTestConnection");
        }

        public static IConnectionDescription GetMySqlConnectionDescription()
        {
            return new ConnectionDescription(ConnectionTypeEnum.MySql, "MySQLTestConnection");
        }

        public class IntegrationTestConfigurationPoco
        {
            public bool IntegrationTestsEnabled { get; set; }
            public string MySqlTestConnectionString { get; set; }
            public string SqlServerTestConnectionString { get; set; }

            public IntegrationTestConfigurationPoco(bool integrationTestsEnabled)
            {
                IntegrationTestsEnabled = integrationTestsEnabled;
            }

            public IntegrationTestConfigurationPoco()
            {
            }
        }

        public static bool Available()
        {
            return Configuration.IntegrationTestsEnabled;
        }
    }
}