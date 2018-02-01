using System;
using System.IO;

namespace Delineate.Cloud.Fast
{
    public abstract class NullCommand: Command
    {
        protected override void Prepare()
        {
            Console.WriteLine("No matching command was found");
        }
    }
}