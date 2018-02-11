using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Help
{
    public class CommandHelpSection : BaseHelpSection
    {
        protected override bool IsVisible
        {
            get{ return ! Context.Info.IsDefault; }
        }

        protected override void AddDetail()
        {
            string detail = Context.Info.Key.Replace(":", " ") + " [OPTIONS]";
            Context.Messages.Success(detail);
        }
    }
}