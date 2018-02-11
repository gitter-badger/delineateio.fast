using System;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
                args = new string[]{"init"};

            try
            {
                Command command = CommandFactory.Create(args);
                command.Outputs.OnFlushed += new MessageEventHandler(Output);
                command.Execute(args );
                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                ConsoleFormatter.WriteException(exception);
                Environment.Exit(1);
            }
        }

        static void Output(object sender, MessageEventArgs e)
        {
            ConsoleFormatter.Write(e);
        }
    }
}