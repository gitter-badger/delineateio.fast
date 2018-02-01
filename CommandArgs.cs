using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    public sealed class CommandArgs 
    {
        private Dictionary<string, List<string>> Args { get; set; } 

        public CommandArgs()
        {
            Args = new Dictionary<string, List<string>>();
        }

        public bool Has(string key)
        {
            return Args.ContainsKey(key);
        }

        public bool IsForced
        {
            get{ return Args.ContainsKey(CommandOptions.FORCE); }
        }

        public bool IsHelp
        {
            get { return Args.ContainsKey(CommandOptions.HELP); }
        }

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
        public List<string> Get(string key)
        {
            return Args[key];
        }
    }    
}
