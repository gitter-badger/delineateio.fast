using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class DescriptionBuilder : BaseBuilder
    {
        public DescriptionBuilder(Command command) : base(command) { }

        public override void Output()
        {
            if(! Command.Info.IsDefault)
            {
                AddHeader("DESCRIPTION");

                Command.Outputs.Indent(2);
                Command.Outputs.SendNormal(Command.Info.Description, isNested: false);
                Command.Outputs.Unindent(2);
            }
        }
    }
}