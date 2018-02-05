using System;
using Delineate.Fast.Commands;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {  
            args = new string[]{"cloud", "init", "-t", "dev"};
            args = new string[]{"cloud", "run"};
            args = new string[]{"cloud", "clean", "-f"};
            
            ProgramArgs programArgs = new ProgramArgs(args);

            if(programArgs.HasArgs)
            {
                try
                {
                    Command command = CommandFactory.Create(programArgs.Values);
                    command.OnOutput += new OutputEventHandler(Output);
                    command.Execute( programArgs.Values );
                }
                catch (Exception exception)
                {
                    ProgramWriter.Write(exception.Message);
                }
            }
            else
            {
                ProgramWriter.Write("No args provided", ConsoleColor.Red, 1);
            }
        }

        static void Output(object sender, OutputEventArgs e)
        {
            ProgramWriter.Write(e);
        }
    }
}