using System;
using Delineate.Fast.Core.Messages;
using Microsoft.Extensions.Logging;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Container for the current context
    /// </summary>
    public class CommandLogger : ICommandLogger
    {
        private ILoggerFactory LoggerFactory { get; set; }

        private ILogger Logger {get; set;}

        public CommandLogger()
        {
            LoggerFactory = new LoggerFactory()
                .AddDebug()
                .AddFile("logs/log.txt");

            Logger = LoggerFactory.CreateLogger<CommandLogger>();
        }

        public void Log(string text)
        {
            Logger.LogInformation(text);
        }
    }
}