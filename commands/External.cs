using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast.Commands
{
    public static class External
    {
        public static void Run(string program, string[] args)
        {
            var cmd = string.Join(" ", args);
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = program,
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            ExternalResult result = new ExternalResult()
            {
                Code = process.ExitCode,
                Output = process.StandardOutput.ReadToEnd()
            };

            process.WaitForExit();
        }
    }
}