using System;
using Delineate.Fast.Commands;
using Delineate.Fast.Nodes;

namespace Delineate.Fast.Cloud.Commands
{
    /// <summary>
    /// Init command sets 
    /// </summary>
    [CommandMatch(Key="cloud:init")]
    public sealed class InitCommand : Command
    {
        #region AddOptions

        /// <summary>
        /// Override that adds a multi option for the applications to be used
        /// </summary>
        protected override void AddOptions()
        {
            Options.Add("-t", "Adds the applications to be used", aliases: "--template");
        }

        #endregion

        #region Prepare
        
        /// <summary>
        /// Override that sets up the .fast directory and gets the config details
        /// </summary>
        protected override void Prepare()
        {
            DirectoryNode fastDir = Root.Add<DirectoryNode>(".fast");
            fastDir.Add<FileNode>("config.yml");
        }

        #endregion
    }
}