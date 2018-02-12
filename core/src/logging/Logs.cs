using System;
using Microsoft.Extensions.Logging;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Logging
{
    /// <summary>
    /// Container for the current context
    /// </summary>
    public sealed class Logs : ILogs
    {
        /// <summary>
        /// The .NET Core Log Factory
        /// </summary>
        /// <returns>The factory</returns>
        private ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// The .NET Core Logger instance
        /// </summary>
        /// <returns>The Logger</returns>        
        private ILogger Logger {get; set;}

        /// <summary>
        /// The constructor for setting up the simple 
        /// loggers (file, debugger to console)
        /// </summary>
        public Logs()
        {
            LoggerFactory = new LoggerFactory()
                .AddDebug()
                .AddFile("logs/log.txt");

            Logger = LoggerFactory.CreateLogger<Logs>();
        }

        /// <summary>
        /// Performs the log and writes the entry
        /// </summary>
        /// <param name="text">The text to write to the logger</param>
        /// <param name="values">Optional array of additional values to format</param>
        public void Log(string text, params string[] values)
        {
            if( values != null && values.Length > 0 )
                text = string.Format(text, values);
            
            Logger.LogInformation(text);
        }
    }
}