using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Help
{
    public class DocumentationBuilder : BaseBuilder
    {
        public DocumentationBuilder(Command command) : base (command) { }

        public override void Output()
        {
            AddHeader("DOCUMENTATION");

            string helpResource = string.Concat(
                                        "http://www.delineate.io/fast/",
                                        Command.Info.Key.Replace(":", "/"));

            Command.Outputs.Indent(2);
            Command.Outputs.SendLink(helpResource, isNested: false);
            Command.Outputs.Unindent(2);
        }
    }
}