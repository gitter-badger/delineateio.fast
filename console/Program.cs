using System;
using Delineate.Fast.Commands;

namespace Delineate.Fast.Console
{
    class Program
    {
        static void Main(string[] args)
        {  
            args = new string[]{"cloud", "init", "-a", "circleci", "-a", "github", "-a", "docker", "-a", "packer", "-a", "terraform"};
            //args = new string[]{"cloud", "run"};
            //args = new string[]{"cloud", "clean", "-f"};
            
            ProgramArgs programArgs = new ProgramArgs(args);

            if(programArgs.HasArgs)
            {
                try
                {
                    Command command = CommandFactory.Create(programArgs.Values);
                    command.Execute( programArgs.Values );
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
