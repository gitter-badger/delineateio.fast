using System;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Wrapper class for managing a list of available options
    /// </summary>
    public sealed class CommandOptions
    {   
        #region Properties 

        /// <summary>
        /// Dictionary of the available options
        /// </summary>
        /// <returns>Collection of options keyed on the primary pair</returns>
        private Dictionary<string, CommandOption> Options = new Dictionary<string, CommandOption>();

        /// <summary>
        /// Access via the alias key to the options
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> Aliases = new Dictionary<string, string>();

        #endregion

        /// <summary>
        /// Adds an option to the collection
        /// </summary>
        /// <param name="option">The option to add</param>
        public void Add(CommandOption option)
        {            
            Options.Add(option.Key, option);
            Aliases.Add(option.Key, option.Key);

            if(option.Aliases != null && option.Aliases.Length > 0)
                foreach(string alias in option.Aliases.Split(","))
                    Aliases.Add(alias, option.Key);
        }

        /// <summary>
        /// Returns the Option object with the details of the option
        /// </summary>
        /// <param name="key">The key of the option to return</param>
        /// <returns>
        /// Returns the requested option details,
        /// if the option is available returns null
        /// </returns>
        public CommandOption Get(string key)
        {
            if(Has(key))
            {
                string secondaryKey = Aliases[key];
                return Options[secondaryKey];
            }
            else
                return null;
        }
        
        /// <summary>
        /// Indoicates if a specific option is present
        /// </summary>
        /// <param name="key">The key of the option</param>
        /// <returns>Returns true if the option available</returns>
        public bool Has(string key)
        {
            return Aliases.ContainsKey(key);
        }
    }
}