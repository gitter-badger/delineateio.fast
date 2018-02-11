using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core
{
    /// <summary>
    /// Command to clean the current Fast environment
    /// </summary>
    /// <remarks>
    /// The -f option can be used to force the environment clean
    /// </remarks>
    [CommandInfo(Key="clean", Description="Cleans the fast environment", IsCore=true)]
    [CommandOption(Key="-f", Description="Forces clean of the fast environment", Aliases="--force")]
    public sealed class CleanCommand : Command
    {
        /// <summary>
        /// Prepares clean tree based on the current configuration
        /// </summary>
        protected internal override void Prepare()
        {
            Root.Add<DirectoryNode>(".fast", operation : NodeOperation.Delete);
        }
    }
}