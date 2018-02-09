using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class VersionsBuilder : BaseBuilder
    {
        public VersionsBuilder(Command command) : base (command) { }

        public override void Output()
        {
            AddHeader("VERSIONS");

            Command.Outputs.Indent(2);

            if( ! Command.Info.IsCore)
                Command.Outputs.SendInfo(Utils.GetPluginVersion(Command.GetType()), false);
            
            Command.Outputs.SendInfo(Utils.GetFastVersion(), false);
            Command.Outputs.SendInfo(Utils.GetDotNetVersion(), false);

            Command.Outputs.Unindent(2);
        }
    }
}