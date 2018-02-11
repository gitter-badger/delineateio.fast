using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class DocumentationHelpSection : BaseHelpSection
    {
        protected override void AddDetail()
        {
            string helpResource = string.Concat(
                                        "http://www.delineate.io/fast/",
                                        Command.Info.Key.Replace(":", "/"));

            Messages.Link(helpResource);
        }
    }
}