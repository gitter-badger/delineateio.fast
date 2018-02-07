using System;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Templates.Local
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="local:init")]
    [CommandOption(PrimaryKey="-t", Description="Template to use during initialisation", HasValue=true, Aliases="--template")]
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

            DirectoryNode circleDir = Root.Add<DirectoryNode>(".circleci");
            FileNode circleFile = circleDir.Add<FileNode>("config.yml");
            circleFile.AddTemplate("circleci.yml");

            
            Root.Add<DirectoryNode>(".github");
            
            DirectoryNode devDir = Root.Add<DirectoryNode>("dev");
            devDir.Add<DirectoryNode>("tests");
            
            
            FileNode composeFile = devDir.Add<FileNode>("docker-compose.yaml");
            composeFile.AddTemplate("application.yml");

            DirectoryNode opsDir = Root.Add<DirectoryNode>("ops");
            opsDir.Add<DirectoryNode>("deploy");
            opsDir.Add<DirectoryNode>("provision");
            opsDir.Add<DirectoryNode>("tests");

            Root.Add<DirectoryNode>("tests");
        }

        #endregion
    }
}