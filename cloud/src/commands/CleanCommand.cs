using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Commands;
using Delineate.Fast.Nodes;

namespace Delineate.Fast.Cloud.Commands
{
    /// <summary>
    /// Command to clean the current project and remove
    /// </summary>
    /// <remarks>
    /// The -f option can be used to force the clean
    /// </remarks>
    [CommandMatch(Key="cloud:clean")]
    public sealed class CleanCommand : Command
    {
        /// <summary>
        /// Add Force command option
        /// </summary>
        protected override void AddOptions() => Options.Add("-f", "Cleans the current project", aliases: "--force");

        /// <summary>
        /// Prepares clean tree based on the current configuration
        /// </summary>
        protected override void Prepare()
        {
            Root.Add<DirectoryNode>(".fast", operation : NodeOperation.Delete);
            
            if(HasParameter("circleci"))
                Root.Add<DirectoryNode>(".circleci", operation : NodeOperation.Delete);

            if(HasParameter("github"))
                Root.Add<DirectoryNode>(".github", operation : NodeOperation.Delete);
            
            Root.Add<DirectoryNode>("dev", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>("ops", operation : NodeOperation.Delete);
            Root.Add<DirectoryNode>("tests", operation : NodeOperation.Delete);
        }
    }
}