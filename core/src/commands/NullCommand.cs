using System;
using System.IO;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Null command is returned if there 
    /// </summary>
    public sealed class NullCommand: Command
    {
        #region Prepare

        /// <summary>
        /// Override of the prepared statement 
        /// </summary>
        protected override void Plan()
        {
            Output("No matching command was found", ConsoleColor.Red, 1);
        }

        #endregion
    }
}