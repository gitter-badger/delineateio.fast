using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// The command arguments 
    /// </summary>
    public sealed class CommandArgs : ICommandArgs
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

        /// <summary>
        /// Contructor that inherrently parses the program args
        /// </summary>
        /// <param name="args">The args provided to the program</param>
        /// <param name="context">The context that contains the valid options</param>
        public CommandArgs(string[] args, CommandContext context)
        {
            Parse(args, context);
        }

        /// <summary>
        /// Parses the program args and validates
        /// </summary>
        /// <param name="programArgs">The program args</param>
        /// <param name="context">The execution context</param>
        private void Parse(string[] programArgs, CommandContext context)
        {
            for(int i = 0; i < programArgs.Length; i++)
            {
                string key = programArgs[i];

                if(key.StartsWith("-"))
                {
                    if( ! context.Options.Has(key))
                        HasErrors = true;

                    Add(key, null);
                }
            }
        }

        /// <summary>
        /// Adds the arg to the args collection 
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value of the arg</param>
        public void Add(string key, string value = null)
        {
            Args.Add(key, value);
        }

        /// <summary>
        /// Checks if the arg exists
        /// </summary>
        /// <param name="key">The arg key</param>
        /// <returns>Return true if arg is set</returns>
        public bool Has(string key)
        {
            return Args.ContainsKey(key);
        }

        /// <summary>
        /// Returns the collection of values for the arg
        /// </summary>
        /// <param name="key">The  key to return the value for</param>
        /// <returns>The value</returns>
        public string Get(string key)
        {
            return Args[key];
        }
    }    
}
