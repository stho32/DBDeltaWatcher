using System;
using System.IO;
using CommandLine;
using DbDeltaWatcher.Classes;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Classes.Database.Aggregations;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Classes.Database.SqlServerSupport;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!TryParseCommandLineOptions(args, out var options)) return;

            SetupEnvironmentConfigurationFromOptions(
                options, 
                out var configurationProvider, 
                out var connectionStringProvider);

            var masterConnectionString = configurationProvider.GetMasterConnectionString();
            if (string.IsNullOrWhiteSpace(masterConnectionString))
            {
                CreateProposalForAConfigurationFile();
                return;
            }

            var repositoryFactory = new SqlServerBasedRepositoryFactory(masterConnectionString);
            
            var taskRepository = repositoryFactory.TaskRepository;
            var tasks = taskRepository.GetList();
            var databaseSupport = new DatabaseSupport(
                new IDatabaseSupport[]
                {
                    new SqlServerDatabaseSupport(connectionStringProvider),
                    new MySqlServerDatabaseSupport(connectionStringProvider)
                });
            
            Console.WriteLine($"{tasks.Length} tasks found.");

            for (var i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"  - processing task {i}/{tasks.Length} {task.ProcessInformation.ProcessName}");
                
                var taskProcessor = new TaskProcessor(
                    task,
                    databaseSupport.GetDatabaseConnection(task.SourceConnection));
                
                taskProcessor.Execute();
            }
        }

        private static void CreateProposalForAConfigurationFile()
        {
            var filePath = Path.Join(AppContext.BaseDirectory, "config.json");
            
            var configurationProvider = new JsonFileBasedConfigurationProvider(filePath);
            configurationProvider.WriteConfiguration();

            Console.WriteLine("I created a config file for you at : " + filePath);
            Console.WriteLine("I cannot continue, the master connection string is not set!");
        }

        private static bool TryParseCommandLineOptions(string[] args, out CommandLineOptions options)
        {
            options = null;
            CommandLineOptions localOptions = null;
            
            var parserResult = Parser.Default.ParseArguments<CommandLineOptions>(args);
            parserResult.WithParsed(co => localOptions = co);
            
            if (parserResult.Tag == ParserResultType.NotParsed)
            {
                Console.WriteLine("Error within parameters.");
                return false;
            }

            options = localOptions;
            return true;
        }

        private static void SetupEnvironmentConfigurationFromOptions(
            CommandLineOptions options,
            out ConfigurationProvider configurationProvider,
            out ConnectionStringProvider connectionStringProvider)
        {
            configurationProvider = new ConfigurationProvider(
                new IConfigurationProvider[]
                {
                    new EnvironmentVariableConfigurationProvider(),
                    new JsonFileBasedConfigurationProvider(Path.Join(AppContext.BaseDirectory, "config.json"))
                }
            );

            connectionStringProvider = new ConnectionStringProvider(
                new IConnectionStringProvider[]
                {
                    new FlatFileConnectionStringProvider(options.FlatFileConnectionStringProviderFilePath, "DBDeltaWatcher")
                }
            );
        }
    }
}