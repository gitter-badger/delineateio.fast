using System;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
                args = new string[]{"-h"};

            try
            {
                Command command = CommandFactory.Create(args );
                command.Outputs.OnOutput += new OutputEventHandler(Output);
                command.Execute( args );
                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                
                ProgramWriter.WriteException(exception);
                Environment.Exit(1);
            }
        }

        static void Output(object sender, OutputEventArgs e)
        {
            ProgramWriter.Write(e);
        }
    }
}