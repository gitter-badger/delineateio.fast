using System;

namespace Delineate.Fast.Cloud
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="cloud:setup")]
    public sealed class SetupCommand : Command
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
                CommandNode circleDir = Root.Add(".circleci");
                circleDir.Add("config.yaml", CommandNodeType.File);
            }

            if(HasParameter("github"))
                Root.Add(".github");
            
            CommandNode devDir = Root.Add("dev");
            devDir.Add("tests");
            if(HasParameter("docker"))
                devDir.Add("docker-compose.yaml", CommandNodeType.File);

            CommandNode opsDir = Root.Add("ops");
            
            if(HasParameter("terraform"))
                opsDir.Add("deploy");
            
            if(HasParameter("packer"))
                opsDir.Add("provision");
            
            opsDir.Add("tests");
            Root.Add("tests");
        }

        #endregion
    }
}