using System;
using System.Reflection;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Class that holds the details of a specific 
    /// command option.  Used in parsing / validating arguments
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public sealed class CommandOption : Attribute
    {
        #region Properties

        /// <summary>
        /// The primary key for the option 
        /// </summary>
        /// <returns>Returns the primary switch</returns>
        public string PrimaryKey { get; set; }
        
        /// <summary>
        /// Description of the option, output when the -h, --help 
        /// option is used for the command 
        /// </summary>
        /// <returns>Returns a user friendly description</returns>
        public string Description { get; set; }
        
        /// <summary>
        /// Comma seperated list of aliases of the option
        /// </summary>
        /// <returns>Returns the list as strings</returns>
        public string Aliases { get; set; }

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