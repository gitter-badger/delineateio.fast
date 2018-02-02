using System;
using System.Collections.Generic;
using System.Resources;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Wrapper class for managing a list of available options
    /// </summary>
    public sealed class CommandOptions
    {   

        #region Constants

        /// <summary>
        /// The force flag
        /// </summary>
        public const string FORCE = "-f";

        /// <summary>
        /// The help flag 
        /// </summary>
        public const string HELP = "-h";

        #endregion

        #region Properties 

        /// <summary>
        /// Dictionary of the available options
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, CommandOption> Options = new Dictionary<string, CommandOption>();

        #endregion
        
        /// <summary>
        /// Comnstuctor to create the container of the options
        /// </summary>
        /// <remarks>
        /// Adds the global options that apply across all commands
        /// </remarks>
        public CommandOptions()
        {
            // Adds the default options for all commands
            Add(HELP, "Provides help for the requested command");
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
                        bool isMandatory = false, params string[] alias)
        {

            CommandOption option = new CommandOption()
            {
                Description = description,
                HasValue = hasValue,
                IsMandatory = isMandatory,
                Alias = alias
            };

            Options.Add(key, option);

            //TODO: Loop around the alias
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
                return Options[key];
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
            return Options.ContainsKey(key);
        }
    }
}