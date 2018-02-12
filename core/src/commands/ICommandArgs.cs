using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandArgs
    {
        /// <summary>
        /// Indicate if the args have errors 
        /// </summary>
        /// <returns>Returns true if no erros</returns>
        bool HasErrors { get; }

        /// <summary>
        /// Adds the arg to the args collection 
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value of the arg</param>
        void Add(string key, string value = null);

        /// <summary>
        /// Checks if the arg exists
        /// </summary>
        /// <param name="key">The arg key</param>
        /// <returns>Return true if arg is set</returns>
        bool Has(string key);

        /// <summary>
        /// Returns the collection of values for the arg
        /// </summary>
        /// <param name="key">The  key to return the value for</param>
        /// <returns>The value</returns>
        string Get(string key);
    }
}