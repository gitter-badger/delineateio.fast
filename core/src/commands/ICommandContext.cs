using System;

using Delineate.Fast.Core.Messaging;
using Delineate.Fast.Core.Logging;

namespace Delineate.Fast.Core.Commands
{
    public interface ICommandContext
    {
        /// <summary>
        /// The logger of the context
        /// </summary>
        /// <returns></returns>
        ILogs Logs { get; }

        /// <summary>
        /// The object tasked with reportining messages 
        /// </summary>
        /// <returns>The instance of the messager</returns>
        IMessenger Messenger { get; }

        /// <summary>
        /// Available options for the current command
        /// </summary>
        /// <returns>The available options for the command</returns>
        ICommandOptions Options {get; }

        /// <summary>
        /// Info about the command
        /// </summary>
        /// <returns></returns>
        ICommandInfo Info { get; }

        /// <summary>
        /// Args for the command execution
        /// </summary>
        /// <returns>Returns the args object</returns>
        ICommandArgs Args { get; }

        /// <summary>
        /// The command being run in the current context
        /// </summary>
        /// <returns></returns>
        Command Command {get; }
    }
}