using System;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Container for the current context
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// The info for the command
        /// </summary>
        /// <returns></returns>
        public CommandInfo Info { get; set; }

        /// <summary>
        /// The command that is being executed
        /// </summary>
        /// <returns></returns>
        public Command Command { get; set; }

        /// <summary>
        /// The Outputs manager for the command
        /// </summary>
        /// <returns></returns>
        public MessageManager Outputs { get; set; }
    }
}