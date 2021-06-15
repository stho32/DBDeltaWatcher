using System;

namespace DbDeltaWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var masterConnection = Environment.GetEnvironmentVariable("DBDELTAWATCHERCONNECTION");
            Console.WriteLine("Connecting using : " + masterConnection??"");
        }
    }
}