using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast.Programs
{
    public static class ExternalProgram
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

            ExternalProgramResult result = new ExternalProgramResult()
            {
                Code = process.ExitCode,
                Output = process.StandardOutput.ReadToEnd()
            };

            process.WaitForExit();
        }
    }
}