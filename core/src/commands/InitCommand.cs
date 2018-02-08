using System;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="init")]
    public sealed class InitCommand : Command
    {
        #region Prepare

        /// <summary>
        /// Overide of the Prepare creates the correct 
        /// directories and files for the config
        /// </summary>
        protected override void Prepare()
        {
            DirectoryNode fastDir = Root.Add<DirectoryNode>(".fast");
            fastDir.Add<FileNode>("config.yml");
        }

        #endregion
    }
}