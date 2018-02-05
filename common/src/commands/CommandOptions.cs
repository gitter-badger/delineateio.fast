using System;
using System.Collections.Generic;
using System.Resources;

namespace Delineate.Fast.Commands
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
        /// Constructor to create the container of the options
        /// </summary>
        /// <remarks>
        /// Adds the global options that apply across all commands
        /// </remarks>
        public CommandOptions()
        {
            // Adds the default options for all commands
            Add("-h", "Provides help for the requested command", aliases: "--help");
        }

        /// <summary>
        /// Adds an option to the collection
        /// </summary>
        /// <param name="key">The key of the option</param>
        /// <param name="description">A description for the option</param>
        /// <param name="type">The type of the option (global, command specific)</param>
        /// <param name="hasValue">Indicates if the option requires a value</param>
        /// <param name="isMandatory">Indicates if the option is mandatory</param>
        /// <param name="alias">Any aliases for the option (e.g.-f, --force)</param>
        public void Add(string key, string description, bool hasValue = false, 
                        bool isMandatory = false, params string[] aliases)
        {
            
            // Creates the option 
            CommandOption option = new CommandOption()
            {
                PrimaryKey = key,
                Aliases = aliases,
                Description = description,
                HasValue = hasValue,
                IsMandatory = isMandatory,
            };
            
            Options.Add(key, option);
            Aliases.Add(key, key);

            if(aliases != null)
                foreach(string alias in aliases)
                    Aliases.Add(alias, key);
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