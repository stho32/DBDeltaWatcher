using System;
using System.IO;
using CommandLine;
using DbDeltaWatcher.Classes;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Classes.Database;
using DbDeltaWatcher.Interfaces;
using DbDeltaWatcher.Interfaces.Configuration;
using DbDeltaWatcher.Interfaces.Database;

namespace DbDeltaWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am DeltaWatcher. I watch Deltas (*bold statement*) :)");

            CommandLineOptions options = null;
            var parserResult = Parser.Default.ParseArguments<CommandLineOptions>(args);
            parserResult.WithParsed(co => options = co);
            if (parserResult.Tag == ParserResultType.NotParsed)
            {
                Console.WriteLine("Error within parameters.");
                return;
            }           
            
            var configurationProvider = new ConfigurationProvider(
                new IConfigurationProvider[]
                {
                    new EnvironmentVariableConfigurationProvider(),
                    new JsonFileBasedConfigurationProvider(Path.Join(AppContext.BaseDirectory, "config.json"))
                }
            );

            var connectionStringProvider = new ConnectionStringProvider(
                new IConnectionStringProvider[]
                {
                    new FlatFileConnectionStringProvider(options.FlatFileConnectionStringProviderFilePath, "DBDeltaWatcher")
                }
            );
            

            if (string.IsNullOrWhiteSpace(configurationProvider.GetMasterConnectionString()))
            {
                File.WriteAllText(Path.Join(AppContext.BaseDirectory, "config.json"), "{ \"MasterConnectionString\" : \"\" }");
                Console.WriteLine("I created a config file for you at : " + Path.Join(AppContext.BaseDirectory, "config.json"));
                Console.WriteLine("Please fill out the content to make me work for you :).");
            }
            
            var masterConnection = configurationProvider.GetMasterConnectionString();
            if (string.IsNullOrWhiteSpace(masterConnection))
            {
                Console.WriteLine("I cannot continue, the master connection string is not set!");
                return;
            }

            IFactory factory = new Factory(
                configurationProvider, 
                connectionStringProvider);
            
            var taskRepository = factory.RepositoryFactory().TaskRepository;

            var tasks = taskRepository.GetList();
            
            Console.WriteLine($"{tasks.Length} tasks found.");

            foreach (var task in tasks)
            {
                var taskProcessor = factory.CreateTaskProcessor(task, factory);
                taskProcessor.Execute();
            }
        }
    }
}