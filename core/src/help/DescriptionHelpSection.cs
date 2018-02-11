using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class DescriptionHelpSection : BaseHelpSection
    {
        protected override bool IsVisible
        {
            get{ return !Command.Info.IsDefault; }
        }

        protected override void AddDetail()
        {
            Messages.Normal(Command.Info.Description);
        }
    }
}