using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Fast
{
    class Program
    {
        static void Main(string[] args)
        {  
            args = new string[]{"init"};
            CommandArgs commandArgs = new CommandArgs(args);
        
            try
            {
                Command command = CommandFactory.Create(commandArgs);
                command.Execute( commandArgs );
            }
            catch (Exception exception)
            {
                ConsoleWriter.Write(exception.Message);
            }
        }
    }
}
