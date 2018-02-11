using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;
using Delineate.Fast.Core.Versioning;

namespace Delineate.Fast.Core.Help
{
    public class VersionsHelpSection : BaseHelpSection
    {
        protected override void AddDetail()
        {
            if( ! Context.Info.IsCore)
                Context.Messages.Info(VersionManager.GetPluginVersion(Context.Command.GetType()));
            
            Context.Messages.Info(VersionManager.GetFastVersion());
            Context.Messages.Info(VersionManager.GetDotNetVersion());
        }
    }
}