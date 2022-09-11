using NLog;
using System;

namespace ApartmentCrawler
{
    internal class Program
    {
        static void Main()
        {
            ConfigNlog();

            Crawler crawler = new();
            crawler.Start();
            //  crawler.Dev();

        }
        static void ConfigNlog()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"Logs/{DateTime.UtcNow}.log", ArchiveAboveSize = 200000 };
            var logconsole = new NLog.Targets.ColoredConsoleTarget("logconsole");


            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }
    }
}