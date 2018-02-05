using System;
using Delineate.Fast.Commands;
using Delineate.Fast.Nodes;

namespace Delineate.Fast.Cloud.Commands
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="cloud:run")]
    public sealed class RunCommand : Command
    {
        #region Prepare

        /// <summary>
        /// Overide of the Prepare creates the correct 
        /// directories and files for the config
        /// </summary>
        protected override void Prepare()
        {
            if(HasParameter("circleci"))
            {
                DirectoryNode circleDir = Root.Add<DirectoryNode>(".circleci");
                circleDir.Add<FileNode>("config.yml")
                        .AddTemplate("circleci.yml");
            }

            if(HasParameter("github"))
                Root.Add<DirectoryNode>(".github");
            
            DirectoryNode devDir = Root.Add<DirectoryNode>("dev");
            devDir.Add<DirectoryNode>("tests");
            if(HasParameter("docker"))
            {
                devDir.Add<FileNode>("docker-compose.yaml")
                        .AddTemplate("application.yml");
            }

            DirectoryNode opsDir = Root.Add<DirectoryNode>("ops");
            
            if(HasParameter("terraform"))
                opsDir.Add<DirectoryNode>("deploy");
            
            if(HasParameter("packer"))
                opsDir.Add<DirectoryNode>("provision");
            
            opsDir.Add<DirectoryNode>("tests");
            Root.Add<DirectoryNode>("tests");
        }

        #endregion
    }
}