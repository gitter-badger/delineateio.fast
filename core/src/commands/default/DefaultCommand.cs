using System;
using System.IO;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Default command is returned if there no more relevant command
    /// </summary>
    [CommandInfo(Key="", IsDefault=true, IsCore=true)]
    public sealed class DefaultCommand: Command
    {
        #region Prepare

        /// <summary>
        /// Override of the prepared statement 
        /// </summary>
        protected override void Plan()
        {
            DisplayHelp();
        }

        #endregion
    }
}