using System;

using Delineate.Fast.Core.Messaging;
using Delineate.Fast.Core.Logging;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Class that represents the current context 
    /// of the exceution 
    /// </summary>
    public class CommandContext : ICommandContext
    {
        /// <summary>
        /// The logger of the context
        /// </summary>
        /// <returns></returns>
        public ILogs Logs { get; set; }

        /// <summary>
        /// The messages 
        /// </summary>
        /// <returns></returns>
        public IMessenger Messenger {get; set;}

        /// <summary>
        /// Available options for the current command
        /// </summary>
        /// <returns>The available options for the command</returns>
        public ICommandOptions Options {get; set;}

        /// <summary>
        /// Info about the command
        /// </summary>
        /// <returns></returns>
        public ICommandInfo Info { get; set; }

        /// <summary>
        /// Args for the command execution
        /// </summary>
        /// <returns>Returns the args object</returns>
        public ICommandArgs Args {get; set;}

        /// <summary>
        /// The command being run in the current context
        /// </summary>
        /// <returns></returns>
        public Command Command {get; set;}

        public CommandContext()
        {
            Logs = new Logs();
            Messenger = new Messenger();
            Options = new CommandOptions();
        }
    }
}
