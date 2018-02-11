using System;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Versioning
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
            Context.Messages.Blank();
            Context.Messages.Indent();

            // The Fast framework version
            Context.Messages.Normal(VersionManager.GetFastVersion());

            /// If not core then the plugin version 
            if( ! Context.Info.IsCore)
                Context.Messages.Normal(VersionManager.GetPluginVersion(GetType()));
            
            /// The .NET Core Framework version 
            Context.Messages.Normal(VersionManager.GetDotNetVersion());

            Context.Messages.Unindent();
        }
    }
}