using CommandLine;

namespace DbDeltaWatcher
{
    public class CommandLineOptions
    {
        [Option(shortName: 'c', longName: "config", Required = false, HelpText = "Path to json based config file", Default = "BASEDIR")]
        public string ConfigFilePath { get; set; }
        
        [Option(shortName: 'p', longName: "cspfile", Required = true, HelpText = "Path to connection string provider based on flat files")]
        public string FlatFileConnectionStringProviderFilePath { get; set; }
    }
}