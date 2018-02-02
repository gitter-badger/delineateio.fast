using System;
using System.Reflection;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Class that holds the details of a specific 
    /// command option.  Used in parsing / validating arguments
    /// </summary>
    public sealed class CommandOption
    {
        #region Properties

        /// <summary>
        /// Description of the option, output when the -h, --help 
        /// option is used for the command 
        /// </summary>
        /// <returns>Returns a user friendly description</returns>
        public string Description { get; set; }
        
        /// <summary>
        /// List of aliases of the option
        /// </summary>
        /// <returns>Returns an unsorted array</returns>
        public string[] Alias { get; set; }

        /// <summary>
        /// Indicates if the option must have a value supplied 
        /// </summary>
        /// <returns>Returns true if a value must be provided with the option</returns>
        public bool HasValue { get; set; }

        /// <summary>
        /// Indicates if the option is mandatory
        /// </summary>
        /// <returns>Returns true if option is mandatory</returns>
        public bool IsMandatory { get; set; }

        #endregion
    }
}