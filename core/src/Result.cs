using System;

namespace Delineate.Fast.Core
{

    public sealed class Result
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
        public string Output { get; set; }
    }
}