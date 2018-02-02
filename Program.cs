using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast
{
    /// <summary>
    /// The program class 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entrypoint of the program
        /// </summary>
        /// <param name="args">Arguments provided from the command line</param>
        static void Main(string[] args)
        {  
            args = new string[]{"init", "-a", "circleci", "-a", "github", "-a", "docker", "-a", "packer", "-a", "terraform"};
            //args = new string[]{"run"};
            //args = new string[]{"clean", "-f"};
            ProgramArgs programArgs = new ProgramArgs(args);

            if(programArgs.HasArgs)
            {
                try
                {
                    Command command = CommandFactory.Create(programArgs);
                    command.Execute( programArgs );
                }
                catch (Exception exception)
                {
                    ConsoleWriter.WriteLine(exception.Message);
                }
            }
            else
            {
                ConsoleWriter.WriteLine("No args provided", ConsoleColor.Red, blank: 1);
            }
        }
    }
}
