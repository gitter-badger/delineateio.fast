using System;
using Delineate.Fast.Core.Versioning;

namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// Version handler that outputs the version numbers
    /// </summary>
    /// <remarks>
    /// When the command is part of the core framework the plugin version is omitted 
    /// <remarks>
    public class VersionCommandHandler : BaseCommandHandler
    {
        /// <summary>
        /// Handles the version request and outputs the rquested 
        /// version information 
        /// </summary>
        protected override void Handle()
        {
            Context.Messenger.Blank();
            Context.Messenger.Indent();

            // The Fast framework version
            Context.Messenger.Normal(VersionManager.GetFastVersion());

            /// If not core then the plugin version 
            if( ! Context.Info.IsCore)
                Context.Messenger.Normal(VersionManager.GetPluginVersion(GetType()));
            
            /// The .NET Core Framework version 
            Context.Messenger.Normal(VersionManager.GetDotNetVersion());

            Context.Messenger.Unindent();
        }
    }
}