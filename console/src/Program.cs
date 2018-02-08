using System;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
                args = new string[]{"local", "clean", "-f"};

            ProgramArgs programArgs = new ProgramArgs(args);

            if(programArgs.HasArgs)
            {
                try
                {
                    ProgramWriter.Write("Running Fast ...", blanks: 1);

                    Command command = CommandFactory.Create(programArgs.Values);
                    command.OnOutput += new OutputEventHandler(Output);
                    command.Execute( programArgs.Values );

                    ProgramWriter.Write("Fast completed!", ConsoleColor.Green, blanks: 1);
                    ProgramWriter.Write("");
                }
                catch (Exception exception)
                {
                    ProgramWriter.Write(exception.Message);
                }
            }
            else
            {
                ProgramWriter.Write("No args provided", ConsoleColor.Red, indent: 1);
            }
        }

        static void Output(object sender, OutputEventArgs e)
        {
            ProgramWriter.Write(e);
        }
    }
}