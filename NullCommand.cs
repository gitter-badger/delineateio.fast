using System;
using System.IO;

namespace Delineate.Fast
{
    public abstract class NullCommand: Command
    {
        protected override void Apply(CommandArgs args)
        {
            Console.WriteLine("No matching command was found");
        }
    }
}