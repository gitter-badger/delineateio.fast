using System;

namespace Delineate.Fast
{
    /// <summary>
    /// Encapulsates the results of the 
    /// </summary>
    public sealed class ExternalResult
    {   
        /// <summary>
        /// The return  code of the external program 
        /// </summary>
        /// <returns>The returned code</returns>
        public int Code { get; set; }

        /// <summary>
        /// The detailed output from the external program 
        /// </summary>
        /// <returns>The full output</returns>
        public string Output { get; set; }
    }
}