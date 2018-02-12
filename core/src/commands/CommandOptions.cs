using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Wrapper class for managing a list of available options
    /// </summary>
    public sealed class CommandOptions: ICommandOptions
    {   
        #region Properties 

        /// <summary>
        /// Dictionary of the available options
        /// </summary>
        /// <returns>Collection of options keyed on the primary pair</returns>
        private SortedDictionary<string, ICommandOption> Options = new SortedDictionary<string, ICommandOption>();

        /// <summary>
        /// Access via the alias key to the options
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> Aliases = new Dictionary<string, string>();

        #endregion

        #region Enumerators

        /// <summary>
        /// Enumerator for the options collection 
        /// </summary>
        /// <returns>Returns the enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Enumerator for the options collection 
        /// </summary>
        /// <returns>Returns the enumerator</returns>
        public IEnumerator<CommandOption> GetEnumerator()
        {
            foreach (CommandOption option in Options.Values)
            {
                yield return option;
            }
        }
        
        #endregion

        /// <summary>
        /// Adds an option to the collection
        /// </summary>
        /// <param name="option">The option to add</param>
        public void Add(ICommandOption option)
        {            
            Options.Add(option.Key, option);
            Aliases.Add(option.Key, option.Key);

            if(option.Aliases != null && option.Aliases.Length > 0)
                foreach(string alias in option.Aliases.Split(","))
                    Aliases.Add(alias, option.Key);
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