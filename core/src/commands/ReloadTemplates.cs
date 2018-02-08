using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="templates:reload")]
    [CommandOption(Key="-s", Description="Shows the loaded commands", Aliases="--show")]
    public sealed class ReloadTemplatesCommand : Command
    {
        /// <summary>
        /// Reloads the templates and then shows the 
        /// </summary>
        protected override void Apply()
        {
            Output("Starting to reload templates...");
            
            SortedDictionary<string, Type> commands = CommandFactory.ReloadTemplates();
            
            foreach( KeyValuePair<string, Type> command in commands)
            {
                Output(command.Key.Replace(":", " "), ConsoleColor.Yellow, 1, 1);
                Output(command.Value.UnderlyingSystemType.FullName, indent: 1);
            }

            Output("Reloading of templates complete", ConsoleColor.Green, 1);
        }
    }
}