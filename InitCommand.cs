using System;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Init command sets 
    /// </summary>
    [CommandMatch(Key="init")]
    public sealed class InitCommand : Command
    {
        #region AddOptions

        /// <summary>
        /// Override that adds a multi option for the applications to be used
        /// </summary>
        protected override void AddOptions()
        {
            //TODO: Add the option description

            Options.Add("-a", "", true);
        }

        #endregion

        #region Prepare
        
        /// <summary>
        /// Override that sets up the .fast directory and gets the config details
        /// </summary>
        protected override void Prepare()
        {
            CommandNode fastDir = Root.Add(".fast");
            fastDir.Add("config.yaml", CommandNodeType.File);
        }

        #endregion

        #region Plan 

        /// <summary>
        /// Overrides the base plan to add the instructions for the user
        /// </summary>
        protected override void Plan()
        {
            base.Plan();

            if(! CanApply )
                ConsoleWriter.WriteLine("The 'Clean' should be used to ensure a clean environment before calling 'init'");
        }

        #endregion
    }
}