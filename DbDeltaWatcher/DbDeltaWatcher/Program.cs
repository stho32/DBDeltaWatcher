using System;
using DbDeltaWatcher.Classes.Configuration;
using DbDeltaWatcher.Interfaces.Configuration;

namespace DbDeltaWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationProvider = new ConfigurationProvider(
                new IConfigurationProvider[] {new EnvironmentVariableConfigurationProvider()}
            );
            
            var masterConnection = configurationProvider.GetMasterConnectionString();
            Console.WriteLine("Connecting using : " + masterConnection??"");
        }
    }
}