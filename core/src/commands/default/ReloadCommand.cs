using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandInfo(Key="reload:plugins", Description="Reloads the fast plugins", IsCore=true)]
    public sealed class ReloadPluginsCommand : Command
    {
        /// <summary>
        /// Reloads the templates and then shows the 
        /// </summary>
        protected override void Apply()
        {
            Outputs.SendNormal("Starting to reload templates...");
            
            SortedDictionary<string, Type> commands = CommandFactory.ReloadTemplates();
            
            Outputs.Indent();
            
            foreach( KeyValuePair<string, Type> command in commands)
            {
                Outputs.SendBlank();
                Outputs.SendWarning(command.Key.Replace(":", " "));
                Outputs.SendNormal(command.Value.UnderlyingSystemType.FullName);
            }

            Outputs.Unindent();
            Outputs.SendBlank();
            Outputs.SendSuccess("Reloading of templates complete");
        }
    }
}