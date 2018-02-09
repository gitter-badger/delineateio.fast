using System.Text;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class OptionsBuilder : BaseBuilder
    {
        public OptionsBuilder(Command command) : base(command) { }

        public override void Output()
        {
            AddHeader("OPTIONS");

            foreach(CommandOption option in Command.Options.Items)
            {           
                string aliases = "[" + string.Join(",", option.Aliases) + "]";

                StringBuilder builder = new StringBuilder();
                builder.Append(option.Key.PadRight(4));
                builder.Append(aliases.PadRight(12));
                builder.Append(option.Description);

                Command.Outputs.Indent(2);
                Command.Outputs.SendNormal(builder.ToString(), isNested: false);
                Command.Outputs.Unindent(2);
            }
        }
    }
}