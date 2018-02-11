using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;
using Delineate.Fast.Core.Versioning;

namespace Delineate.Fast.Core.Help
{
    public class VersionsHelpSection : BaseHelpSection
    {
        protected override void AddDetail()
        {
            if( ! Command.Info.IsCore)
                Messages.Info(VersionManager.GetPluginVersion(Command.GetType()));
            
            Messages.Info(VersionManager.GetFastVersion());
            Messages.Info(VersionManager.GetDotNetVersion());
        }
    }
}