using System;
using System.Reflection;

namespace Delineate.Fast.Commands
{
    /// <summary>
    /// Decorator attritute to identify commands for dynamic laoding 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class CommandMatch : Attribute
    {   
        #region Properties

        /// <summary>
        /// The Key to be used when adding to the collection 
        /// </summary>
        /// <returns>Returns the key as a string</returns>
        public string Key{ get; set;}
    
        #endregion
    }
}