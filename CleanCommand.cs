using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Fast
{
    [CommandMatch(new String[]{"clean"} )]
    public class CleanCommand : Command
    {
        protected override void Apply(CommandArgs args)
        {   
                
        }

        private void ApplyTree()
        {
            File.Delete("fast.config");
            Directory.Delete("tests");
            Directory.Delete("ops");
            Directory.Delete("tests");
        }
    }
}