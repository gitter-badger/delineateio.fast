using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Command args parser
    /// </summary>
    public sealed class CommandArgsParser 
    {
        #region Properties & Fields
        
        /// <summary>
        /// List of warnings when parsing the args
        /// </summary>
        /// <returns>Returns the list of warnings</returns>
        public List<string> Warnings = new List<string>();
        
        /// <summary>
        /// List of errors when parsing the args
        /// </summary>
        /// <returns>Returns the list of errors</returns>
        public List<string> Errors = new List<string>();

        /// <summary>
        /// Indicates if there are warnings
        /// </summary>
        /// <returns>Returns true when warnings were encountered</returns>
        public bool HasWarnings
        {
            get{ return Warnings.Count > 0; } 
        }

        /// <summary>
        /// Indicates if there are errors
        /// </summary>
        /// <returns>Returns true when errors encounetred</returns>
        public bool HasErrors 
        { 
            get{ return Errors.Count > 0; } 
        }

        #endregion

        /// <summary>
        /// Parses the args p[rovided]
        /// </summary>
        /// <param name="programArgs">The program args provided</param>
        /// <param name="options">The valid options for the command</param>
        /// <param name="args">The command args</param>
        public void Parse(ProgramArgs programArgs,CommandOptions options, CommandArgs args)
        {
            //TODO: Refactor 

            for(int i = 0; i < programArgs.Values.Length; i++)
            {
                string key = programArgs.Values[i];

                if(key.StartsWith("-"))
                {
                    if( ! options.Has(key))
                    {
                        AddMessage(Errors, "{0} is not a valid option ", key);
                    }
                    else
                    {
                        CommandOption option = options.Get(key);

                        if(option.HasValue)
                        {
                            args.Add(key, programArgs.Values[ i + 1]);
                            i++;
                        }
                        else
                        {
                            args.Add(key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds a message
        /// </summary>
        /// <param name="output">The collection to add the message to</param>
        /// <param name="text">The text to be added</param>
        /// <param name="value">The value of the option</param>
        private void AddMessage(List<string> output, string text, string value)
        {
            output.Add(string.Format(text, value));
        }
    }
}