using System;
using System.IO;

namespace Delineate.Fast
{
    [CommandMatch(new String[]{"init"} )]
    public class InitCommand : Command
    {
        protected override void Apply(CommandArgs args)
        {
            ApplyTree();
        }

        private void ApplyTree()
        {
            File.Create("fast.config");
            DirectoryInfo devDir = Directory.CreateDirectory("dev");
            devDir.CreateSubdirectory("tests");
            DirectoryInfo opsDir = Directory.CreateDirectory("ops");
            opsDir.CreateSubdirectory("deploy");
            opsDir.CreateSubdirectory("provision");
            opsDir.CreateSubdirectory("tests");
            Directory.CreateDirectory("tests");
        }    
    }
}