using System;
using System.IO;

namespace Delineate.Cloud.Fast
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
        protected override void Prepare()
        {
            ConsoleWriter.WriteLine("No matching command was found", ConsoleColor.Red, blank: 1);
        }

        #endregion
    }
}