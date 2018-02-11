using System;

using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Commands
{
    public class CommandContext
    {
        /// <summary>
        /// The logger of the context
        /// </summary>
        /// <returns></returns>
        public ICommandLogger Logs = new CommandLogger();

        /// <summary>
        /// The messages 
        /// </summary>
        /// <returns></returns>
        public MessageManager Messages = new MessageManager();

        /// <summary>
        /// Available options for the current command
        /// </summary>
        /// <returns>The available options for the command</returns>
        public CommandOptions Options = new CommandOptions();

        /// <summary>
        /// Info about the command
        /// </summary>
        /// <returns></returns>
        public CommandInfo Info { get; set; }

        /// <summary>
        /// Args for the command execution
        /// </summary>
        /// <returns>Returns the args object</returns>
        public CommandArgs Args {get; set;}

        /// <summary>
        /// The command being run in the current context
        /// </summary>
        /// <returns></returns>
        public Command Command {get; set;}
    }
}
