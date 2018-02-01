using System;
using System.Collections;
using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    class Program
    {
        static void Main(string[] args)
        {  
            args = new string[]{"init", "-f"};
            ProgramArgs programArgs = new ProgramArgs(args);
        
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
    }
}
