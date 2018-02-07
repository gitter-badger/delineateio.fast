using System;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {  
            args = new string[]{"local", "init", "-t", "dev"};
            //args = new string[]{"local", "run"};
            //args = new string[]{"local", "clean", "-f"};
            
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