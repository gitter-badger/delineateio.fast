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
        public bool HasError { get; private set; }

        /// <summary>
        /// The standard output from the external program 
        /// </summary>
        /// <returns>The full output</returns>
        public string Output { get; private set; }

        /// <summary>
        /// Creates a tool result
        /// </summary>
        /// <param name="result">Process result</param>
        internal ToolResult(Result result)
        {
            HasError = result.HasError;
            Output = result.Output;
        }
    }
}