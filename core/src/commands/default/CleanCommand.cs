using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Command to clean the current project and remove
    /// </summary>
    /// <remarks>
    /// The -f option can be used to force the clean
    /// </remarks>
    [CommandInfo(Key="clean", Description="Cleans the fast environment", IsCore=true)]
    [CommandOption(Key="-f", Description="Forces the cleaning of the fast environment", Aliases="--force")]
    public sealed class CleanCommand : Command
    {
        /// <summary>
        /// Prepares clean tree based on the current configuration
        /// </summary>
        protected override void Prepare()
        {
            Root.Add<DirectoryNode>(".fast", operation : NodeOperation.Delete);
        }
    }
}