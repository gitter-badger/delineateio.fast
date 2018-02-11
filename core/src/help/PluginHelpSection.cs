using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class PluginHelpSection : BaseHelpSection
    {
        protected override bool IsVisible
        {
            get{ return ! Context.Info.IsDefault && ! Context.Info.IsCore; }
        }

        protected override void AddDetail()
        {
            Context.Messages.Normal(Context.Info.PluginName);
        }
    }
}