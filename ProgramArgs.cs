using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    public sealed class ProgramArgs 
    {
        public const string SEPERATOR = ":";

        public ProgramArgs(String[] args)
        {
            if(args == null || args.Length == 0)
                throw new ArgumentNullException();

            Values = new String[args.Length];    
            for(int i = 0; args.Length > i; i++)
            {   
                Values[i] = args[i].ToLower();
            }
        }

        public String[] Values { get; private set; }
    }    
}
