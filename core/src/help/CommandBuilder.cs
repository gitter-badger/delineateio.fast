using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class CommandBuilder : BaseBuilder
    {
        public CommandBuilder(Command command) : base(command) { }

        public override void Output()
        {
            if(!Command.Info.IsDefault)
            {
                string detail = Command.Info.Key.Replace(":", " ") + " [OPTIONS]";

                AddHeader("COMMAND");

                Command.Outputs.Indent(2);
                Command.Outputs.SendSuccess(detail, isNested: false);
                Command.Outputs.Unindent(2);
            }
        }
    }
}