using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class CommandHelpSection : BaseHelpSection
    {
        protected override bool IsVisible
        {
            get{ return !Command.Info.IsDefault; }
        }

        protected override void AddDetail()
        {
            string detail = Command.Info.Key.Replace(":", " ") + " [OPTIONS]";
            Messages.Success(detail);
        }
    }
}