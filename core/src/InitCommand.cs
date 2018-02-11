using System;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core
{
    /// <summary>
    /// Init command that is used to create the Fast environment 
    /// </summary>
    [CommandInfo(Key="init", Description="Initializes the fast environment", IsCore=true)]
    public sealed class InitCommand : Command
    {
        #region Prepare

        /// <summary>
        /// Overide of the Prepare creates the correct 
        /// directories and files for the Fast environment 
        /// </summary>
        protected internal override void Prepare()
        {
            DirectoryNode fastDir = Root.Add<DirectoryNode>(".fast");
            fastDir.Add<FileNode>("config.yml");
        }

        #endregion
    }
}