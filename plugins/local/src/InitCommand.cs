using System;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Plugins.Local
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandInfo(Key="init:local", Description="Initiatizes the local environment", PluginName="local")]
    [CommandOption(Key="-t", Description="Specifies the template to use when applying", HasValue=true, Aliases="--template")]
    public sealed class InitCommand : Command
    {
        #region Prepare
        
        /// <summary>
        /// Overide of the Prepare creates the correct 
        /// directories and files for the config
        /// </summary>
        protected override void Prepare()
        {
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