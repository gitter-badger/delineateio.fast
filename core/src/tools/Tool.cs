using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast.Core.Tools
{
    /// <summary>
    /// The base class for all tool implementations
    /// </summary>
    public abstract class Tool
    {
        #region Properties

        /// <summary>
        /// The name of the tool to use
        /// </summary>
        /// <returns></returns>
        protected string Name { get; set; }

        /// <summary>
        /// The version of the tool to use
        /// </summary>
        /// <returns></returns>
        public string Version { get; set; }

        #endregion

        /// <summary>
        /// Gets the active version for the tool
        /// </summary>
        /// <returns>Returns the version in the tools format</returns>
        public abstract string GetActiveVersion();

        /// <summary>
        /// Returns a sorted list of installed versions
        /// </summary>
        /// <returns>The list of installed versions</returns>
        public abstract List<string> GetAllVersions(); 

        /// <summary>
        /// Performs the install of the tool
        /// </summary>
        /// <param name="version">The version of the tool to be installed</param>
        public abstract void Install(string version);

        /// <summary>
        /// Uninstalls the specified version of the product
        /// </summary>
        /// <param name="version">The version to be uninstalled</param>
        public abstract void Uninstall(string version);

        /// <summary>
        /// Switches to a particular version 
        /// </summary>
        /// <param name="version"></param>
        public abstract void SwitchTo(string version);

        #region Generic Run Method

        /// <summary>
        /// Runs a command using the tool
        /// </summary>
        /// <param name="args">Arguments for the tool</param>
        protected ToolResult Run(params string[] args)
        {
            if(Name == null || Name.Length == 0)
                throw new ArgumentException("No tool has been specified");

            var cmd = string.Join(" ", args);
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            using(Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo()
                {
                    FileName = Name,
                    Arguments = escapedArgs,
                    // Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.Start();
                process.WaitForExit();

                ToolResult result = new ToolResult()
                {
                    HasError = process.ExitCode > 0,
                    StandardOutput = process.StandardOutput.ReadToEnd(),
                    StandardError = process.StandardError.ReadToEnd(),
                };
                
                return result;
            }
        }

        #endregion
    }
}