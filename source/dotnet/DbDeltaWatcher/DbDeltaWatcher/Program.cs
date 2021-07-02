﻿using System;
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
            if (!TryParseCommandLineOptions(args, out var options)) return;

            SetupEnvironmentConfigurationFromOptions(options, out var configurationProvider, out var connectionStringProvider);

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

        private static ConfigurationProvider SetupEnvironmentConfigurationFromOptions(
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
            return configurationProvider;
        }
    }
}