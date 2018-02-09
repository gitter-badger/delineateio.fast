using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Plugins.Local
{
    /// <summary>
    /// Command to clean the current project and remove
    /// </summary>
    /// <remarks>
    /// The -f option can be used to force the clean
    /// </remarks>
    [CommandInfo(Key="clean:local", Description="Cleans the local environment", PluginName="local")]
    [CommandOption(Key="-f", Description="Cleans the current project", Aliases="--force")]
    public sealed class CleanCommand : Command
    {
        /// <summary>
        /// Prepares clean tree based on the current configuration
        /// </summary>
        protected override void Prepare()
        {
            Root.Add<DirectoryNode>(".circleci", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>(".github", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>("dev", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>("ops", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>("tests", operation : NodeOperation.Delete);
        }
    }
}