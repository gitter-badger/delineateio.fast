using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Fast
{
    /// <summary>
    /// Command to clean the current project and remove
    /// </summary>
    /// <remarks>
    /// The -f option can be used to force the clean
    /// </remarks>
    [CommandMatch(Key="clean")]
    public sealed class CleanCommand : Command
    {
        /// <summary>
        /// Add Force command option
        /// </summary>
        protected override void AddOptions()
        {
            Options.Add(CommandOptions.FORCE, "Cleans the current project");
        }

        /// <summary>
        /// Prepares clean tree based on the current configuration
        /// </summary>
        protected override void Prepare()
        {
            Root.Add(".fast", operation : CommandNodeOperation.Delete);
            
            if(HasParameter("circleci"))
                Root.Add(".circleci", operation : CommandNodeOperation.Delete);

            if(HasParameter("github"))
                Root.Add(".github", operation : CommandNodeOperation.Delete);
            
            Root.Add("dev", operation : CommandNodeOperation.Delete);
            Root.Add("ops", operation : CommandNodeOperation.Delete);
            Root.Add("tests", operation : CommandNodeOperation.Delete);
        }
    }
}