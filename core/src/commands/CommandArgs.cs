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
        private Dictionary<string, List<string>> Args = new Dictionary<string, List<string>>();

        /// <summary>
        /// Indicates if the command is to to be forced
        /// </summary>
        /// <returns>Returns true if -f is present in the options</returns>
        public bool IsForced
        {
            get{ return Args.ContainsKey("-f"); }
        }

        /// <summary>
        /// Indicates that it's a version request
        /// </summary>
        /// <returns></returns>
        public bool IsVersion
        {
            get{ return Args.ContainsKey("-v"); }
        }

        /// <summary>
        /// Indicates if help has been requested for the command
        /// </summary>
        /// <returns>Returns true if -h is present in the options</returns>
        public bool IsHelp
        {
            get { return Args.ContainsKey("-h"); }
        }

        #endregion

        /// <summary>
        /// Adds the option to the arguments 
        /// </summary>
        /// <param name="key">The</param>
        /// <param name="value"></param>
        public void Add(string key, string value = null)
        {
            if( Has( key) )
            {
                List<string> values = Get(key);
                values.Add(value);
            }
            else
            {
                if(value == null)
                {
                    Args.Add(key, null);
                }
                else
                {
                    List<string> values = new List<string>();
                    values.Add(value);
                    Args.Add(key, values);
                }
            }
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
        /// <param name="key">The option key to return the values for</param>
        /// <returns>The list of values</returns>
        public List<string> Get(string key)
        {
            if( ! Has(key) )
                throw new ArgumentException();

            return Args[key];
        }

        /// <summary>
        /// Returns the first item in the args
        /// </summary>
        /// <param name="key">The key for the arg to retrieve</param>
        /// <returns>The item as a string</returns>
        public string GetFirst(string key)
        {
            List<string> items = Get(key);
            
            if(items == null || items.Count == 0)
                throw new ArgumentException();

            return items[0];
        }
    }    
}
