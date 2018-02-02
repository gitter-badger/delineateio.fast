using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast
{
    /// <summary>
    /// Wrapper class for the program arguments
    /// </summary>
    public sealed class ProgramArgs 
    {
        #region Constants

        /// <summary>
        /// Used to seperate values when creating a key to 
        /// use when matching to the commands
        /// </summary>
        public const string SEPERATOR = ":";

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if the program has args
        /// </summary>
        /// <returns>Returns false if no args provided</returns>
        public bool HasArgs { get; private set; }

        /// <summary>
        /// Exposes the values as an array
        /// </summary>
        /// <returns>
        /// An array of the args as provided at thd command line 
        /// </returns>
        public string[] Values { get; private set; }

        #endregion

        /// <summary>
        /// Constructor that processes the provided args
        /// </summary>
        /// <param name="args">Array of the arguements</param>
        public ProgramArgs(String[] args)
        {
            if(args != null || args.Length > 0)
            {
                Values = new string[args.Length];    
                for(int i = 0; args.Length > i; i++)
                {   
                    Values[i] = args[i].ToLower();
                }    

                HasArgs = true;
            }
        }
    }    
}
