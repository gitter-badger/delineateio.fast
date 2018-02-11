using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// The command arguments 
    /// </summary>
    public sealed class CommandArgs 
    {
        #region Properties

        /// <summary>
        /// Internal data structure holding the arguments
        /// </summary>
        /// <returns>Directory keyed on the option with a list of the values</returns>
        private Dictionary<string, string> Args = new Dictionary<string, string>();

        /// <summary>
        /// Indicate if the args have errors 
        /// </summary>
        /// <returns>Returns true if no erros</returns>
        public bool HasErrors { get; private set; }

        #endregion

        public CommandArgs(string[] args, CommandContext context)
        {
            Parse(args, context.Options);
        }

        /// <summary>
        /// Parses the program args and validates
        /// </summary>
        /// <param name="programArgs"></param>
        /// <param name="options"></param>
        public void Parse(string[] programArgs, CommandOptions options)
        {
            for(int i = 0; i < programArgs.Length; i++)
            {
                string key = programArgs[i];

                if(key.StartsWith("-"))
                {
                    if( ! options.Has(key))
                        HasErrors = true;

                    Add(key, null);
                }
            }
        }

        /// <summary>
        /// Adds the option to the arguments 
        /// </summary>
        /// <param name="key">The</param>
        /// <param name="value"></param>
        public void Add(string key, string value = null)
        {
            Args.Add(key, value);
        }

        /// <summary>
        /// Checks if the option exists
        /// </summary>
        /// <param name="key">The option key</param>
        /// <returns>Return true if option is set</returns>
        public bool Has(string key)
        {
            return Args.ContainsKey(key);
        }

        /// <summary>
        /// Returns the collection of values for the option
        /// </summary>
        /// <param name="key">The option key to return the value for</param>
        /// <returns>The value</returns>
        public string Get(string key)
        {
            return Args[key];
        }
    }    
}
