using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class PluginHelpSection : BaseHelpSection
    {
        protected override bool IsVisible
        {
            get{ return ! Command.Info.IsDefault && ! Command.Info.IsCore; }
        }

        protected override void AddDetail()
        {
            Messages.Normal(Command.Info.PluginName);
        }
    }
}