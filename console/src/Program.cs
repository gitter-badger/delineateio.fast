using System;
using System.Diagnostics;

using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messages;

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
                // Creates the command context
                CommandContext context = CommandFactory.Create(args);

                //Writes up events to the messages
                context.Messages.OnFlushed += new MessageEventHandler(Output);
                
                //Executes the command 
                context.Command.Execute(args );
                
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