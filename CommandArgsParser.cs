using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    public sealed class CommandArgsParser 
    {
        public List<string> Warnings { get; set; }
        
        public List<string> Errors {get; private set;}

        public bool HasWarnings
        {
            get{ return Warnings.Count > 0; } 
        }

        public bool HasErrors 
        { 
            get{ return Errors.Count > 0; } 
        }

        public CommandArgsParser()
        {
            Warnings = new List<string>();
            Errors = new List<string>();
        }

        public void Parse(ProgramArgs programArgs,CommandOptions options, CommandArgs args)
        {
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
                        }
                        else
                        {
                            args.Add(key);
                        }
                    }
                }
            }
        }

        private void AddMessage(List<string> output, string text, string value)
        {
            output.Add(string.Format(text, value));
        }
    }
}