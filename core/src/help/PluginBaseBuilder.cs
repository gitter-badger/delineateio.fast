using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class PluginBuilder : BaseBuilder
    {
        public PluginBuilder(Command command) : base  (command) { }

        public override void Output()
        {
            if(! Command.Info.IsDefault && ! Command.Info.IsCore)
            {
                AddHeader("PLUGIN");

                Command.Outputs.Indent(2);
                Command.Outputs.SendNormal(Command.Info.PluginName, false);
                Command.Outputs.Unindent(2);
            }
        }
    }
}