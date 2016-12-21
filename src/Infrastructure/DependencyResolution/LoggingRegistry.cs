using System;
using System.IO;
using Serilog;
using StructureMap;

namespace Infrastructure.DependencyResolution
{
    public class LoggingRegistry : Registry
    {
        public LoggingRegistry()
        {

            var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") ?? string.Empty;


            For<Serilog.ILogger>().Singleton().Use(
                new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile(Path.Combine(dataDirectory.ToString(), @"logs\log-{Date}.txt")).CreateLogger());

            //This is used for Seri to help debug itself. This breaks the build process so use it only when needed locally.
            //var file = File.CreateText(Path.Combine(dataDirectory.ToString(), "SeriLog.txt"));
            //Serilog.Debugging.SelfLog.Out = TextWriter.Synchronized(file);

//            For<AgreementDataAccess.IRecordsCache>().Use<AgreementDataAccess.IRecordsCache ()
        }
    }
}
