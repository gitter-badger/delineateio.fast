using System;
using System.Reflection;
using System.Runtime.Serialization;

using Delineate.Fast.Core.Diagnostics;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Class that holds the details of a specific 
    /// command option.  Used in parsing / validating arguments
    /// </summary>
    [DataContract]
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public sealed class CommandOption : Attribute, IDebuggable
    {
        #region Properties

        /// <summary>
        /// The primary key for the option 
        /// </summary>
        /// <returns>Returns the primary switch</returns>
        [DataMember]
        public string Key { get; set; }
        
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
        [DataMember]
        public string Aliases { get; set; }

        /// <summary>
        /// Indicates if the option must have a value supplied 
        /// </summary>
        /// <returns>Returns true if a value must be provided with the option</returns>
        [DataMember]
        public bool HasValue { get; set; }

        #endregion
    }
}