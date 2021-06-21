using System;
using System.IO;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Interfaces.Configuration;

namespace DbDeltaWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am DeltaWatcher. I watch Deltas (*bold statement*) :)");
            var configurationProvider = new ConfigurationProvider(
                new IConfigurationProvider[]
                {
                    new EnvironmentVariableConfigurationProvider(),
                    new JsonFileBasedConfigurationProvider(Path.Join(AppContext.BaseDirectory, "config.json"))
                }
            );

            if (string.IsNullOrWhiteSpace(configurationProvider.GetMasterConnectionString()))
            {
                File.WriteAllText(Path.Join(AppContext.BaseDirectory, "config.json"), "{ \"MasterConnectionString\" : \"\" }");
                Console.WriteLine("I created a config file for you at : " + Path.Join(AppContext.BaseDirectory, "config.json"));
                Console.WriteLine("Please fill out the content to make me work for you :).");
            }
            
            var masterConnection = configurationProvider.GetMasterConnectionString();
            Console.WriteLine("Connecting using : " + masterConnection??"");
            
        }
    }
}