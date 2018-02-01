using System;
using System.Collections.Generic;
using System.Resources;

namespace Delineate.Cloud.Fast
{
    public sealed class CommandOptions
    {   

        public const string FORCE = "-f";
        public const string HELP = "-h";

        private Dictionary<string, CommandOption> Options { get; set; }

        public CommandOptions()
        {
            Options = new Dictionary<string, CommandOption>();

            // Adds the default options for all commands
            Add(FORCE, "Forces the command to be applied and overrides any warnings", CommandOptionTypes.Global);
            Add(HELP, "Provides help for the requested command", CommandOptionTypes.Global);
        }

        public void Add(string key, string description, 
                        CommandOptionTypes type = CommandOptionTypes.Command, bool hasValue = false, 
                        bool isMandatory = false, params string[] alias)
        {

            CommandOption option = new CommandOption()
            {
                Description = description,
                Type = type,
                HasValue = hasValue,
                IsMandatory = isMandatory,
                Alias = alias
            };

            Options.Add(key, option);
        }

        public CommandOption Get(string key)
        {
            return Options[key];
        }

        public bool Has(string key)
        {
            return Options.ContainsKey(key);
        }
    }
}