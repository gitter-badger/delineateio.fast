using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;
using Delineate.Fast.Core.Versioning;

namespace Delineate.Fast.Core.Help
{
    /// <summary>
    /// Concrete implementation for the version section
    /// </summary>
    public class VersionsHelpSection : BaseHelpSection
    {
        /// <summary>
        /// Adds the version detail
        /// </summary>
        protected override void AddDetail()
        {
            if( ! Context.Info.IsCore)
                Context.Messenger.Info(VersionManager.GetPluginVersion(Context.Command.GetType()));
            
            Context.Messenger.Info(VersionManager.GetFastVersion());
            Context.Messenger.Info(VersionManager.GetDotNetVersion());
        }
    }
}