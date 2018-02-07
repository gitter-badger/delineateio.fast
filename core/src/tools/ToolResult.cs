using System;

namespace Delineate.Fast.Core.Tools
{
    /// <summary>
    /// Encapulsates the results of the tool
    /// </summary>
    public sealed class ToolResult
    {   
        /// <summary>
        /// Indicates if the result has an error
        /// </summary>
        /// <returns>Return true if an error</returns>
        public bool HasError { get; set; }

        /// <summary>
        /// The standard output from the external program 
        /// </summary>
        /// <returns>The full output</returns>
        public string StandardOutput { get; set; }

        /// <summary>
        /// The standard error from the external program 
        /// </summary>
        /// <returns>The full error</returns>
        public string StandardError { get; set; }
    }
}