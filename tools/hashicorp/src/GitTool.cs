/*
using System;

namespace Delineate.Fast.Tools
{ 
    public class GitTool: Tool
    {
        public GitTool() 
        {
            Name = "git";
        }

        public override string GetVersion()
        {
            ToolResult result = Run("--version");

            if(result.HasError )
                throw new ApplicationException();

            string cleanVersion = result.StandardOutput.Substring(12);
            cleanVersion = cleanVersion.Replace(Environment.NewLine, String.Empty);

            return cleanVersion;
        } 

        /// <summary>
        /// Installs a particular version of terraform
        /// </summary>
        /// <param name="version">The version to be installed</param>
        public override void Install(string version)
        {
            ToolResult result = Run("brew", "install", "git");

            if(result.HasError )
                throw new ApplicationException();
        }
    }   
}
*/