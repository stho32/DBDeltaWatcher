using System;
using System.IO;
using CommandLine;
using DbDeltaWatcher.Classes;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Classes.Database.Aggregations;
using DbDeltaWatcher.Classes.Database.MySqlSupport;
using DbDeltaWatcher.Classes.Database.SqlServerSupport;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher
{
    class Program
    {
        private const string AppName = "DbDeltaWatcher";
        
        static void Main(string[] args)
        {
            if (!TryParseCommandLineOptions(args, out var options)) return;

            var configurationProvider = options.CreateConfigurationProvider();

            if (!TryGetMasterConnectionString(configurationProvider, out var masterConnectionString))
            {
                CreateProposalForAConfigurationFile();
                return;
            }

            var repositoryFactory = new SqlServerBasedRepositoryFactory(masterConnectionString);

            var taskRepository = repositoryFactory.TaskRepository;
            var connectionStringProvider = options.CreateConnectionStringProvider(AppName);
            
            var databaseSupport = new DatabaseSupport(new IDatabaseSupport[]{
                    new SqlServerDatabaseSupport(connectionStringProvider),
                    new MySqlServerDatabaseSupport(connectionStringProvider)
                });

            Console.WriteLine("  - looking for tasks to process");
            var tasks = taskRepository.GetList();
            Console.WriteLine($"  - {tasks.Length} tasks found.");

            for (var i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"  - task {i+1}/{tasks.Length} : processing {task.ProcessInformation.ProcessName}");

                var taskProcessor = new TaskProcessor(
                    task,
                    databaseSupport);

                taskProcessor.Execute();
                
                Console.WriteLine($"  - task {i+1}/{tasks.Length} : completed {task.ProcessInformation.ProcessName}");
            }

            Console.WriteLine("  - all tasks have been completed");
        }

        private static bool TryGetMasterConnectionString(IConfigurationProvider configurationProvider,
            out string masterConnectionString)
        {
            masterConnectionString = configurationProvider.GetMasterConnectionString();

            return !string.IsNullOrWhiteSpace(masterConnectionString);
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
    }
}