using System;
using System.Reflection;

using Delineate.Fast.Core.Diagnostics;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Decorator attritute to identify commands for dynamic laoding 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class CommandInfo : Attribute, IDebuggable
    {   
        #region Properties

        /// <summary>
        /// The Key to be used when adding to the collection 
        /// </summary>
        /// <returns>Returns the key as a string</returns>
        public string Key{ get; set;}

        /// <summary>
        /// Returns the description 
        /// </summary>
        /// <returns>The description of the command</returns>
        public string Description { get; set; }

        /// <summary>
        /// Indicates that a command is core 
        /// </summary>
        /// <return>Returns true if the command is c ore</returns>
        public bool IsCore { get; set; }

        /// <summary>
        /// Indicates that the command is the the default command 
        /// </summary>
        /// <returns></returns>
        public bool IsDefault { get; set; }

        /// <summary>
        /// The plugin name is returned
        /// </summary>
        /// <returns>The plugin name</returns>
        public string PluginName { get; set; }
        
        #endregion
    }
}