using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast.Core.Processes
{
    /// <summary>
    /// The base class for all tool implementations
    /// </summary>
    public static class ProcessManager
    {
        /// <summary>
        /// Runs a command using the tool
        /// </summary>
        /// <param name="args">Arguments for the tool</param>
        public static ProcessResult Execute(string processName, params string[] args)
        {
            if(processName == null || processName.Length == 0)
                throw new ArgumentException("No process has been specified");

            var cmd = string.Join(" ", args);
            var escapedArgs = cmd.Replace("\"", "\\\"");

            try
            {            
                using(Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = processName,
                        Arguments = escapedArgs,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    process.Start();
                    process.WaitForExit();

                    return new ProcessResult()
                    {
                        HasError = process.ExitCode > 0,
                        Output = process.StandardOutput.ReadToEnd(),
                    };
                }
            }
            catch
            {
                return new ProcessResult()
                {
                    HasError = true,
                };
            }
        }
    }
}