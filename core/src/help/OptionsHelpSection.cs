using System;
using System.Text;

using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Concrete implementation for displaying the options 
    /// help section
    /// </summary>
    public class OptionsHelpSection : BaseHelpSection
    {
        /// <summary>
        /// Displays the Options detail for each option that 
        /// can be used in conjunction with the command
        /// </summary>
        protected override void AddDetail()
        {
            foreach(CommandOption option in Context.Options)
            {           
                string aliases = "[" + string.Join(",", option.Aliases) + "]";
                string command = Context.Info.Key.Replace(":", " ");

                StringBuilder builder = new StringBuilder();
                builder.Append(option.Key.PadRight(4));
                builder.Append(aliases.PadRight(12));
                builder.Append(string.Format(option.Description, command));

                Context.Messenger.Normal(builder.ToString());
            }
        }
    }
}