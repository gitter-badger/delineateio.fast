using System.Text;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class OptionsHelpSection : BaseHelpSection
    {
        protected override void AddDetail()
        {
            foreach(CommandOption option in Command.Options.Items)
            {           
                string aliases = "[" + string.Join(",", option.Aliases) + "]";
                string command = Command.Info.Key.Replace(":", " ");

                StringBuilder builder = new StringBuilder();
                builder.Append(option.Key.PadRight(4));
                builder.Append(aliases.PadRight(12));
                builder.Append(string.Format(option.Description, command));

                Messages.Normal(builder.ToString());
            }
        }
    }
}