using System;
using System.IO;

namespace Delineate.Fast
{
    [CommandMatch(new String[]{"vm", "remove"} )]
    public abstract class ProvisionCommand : Command
    {
        protected DirectoryInfo Directory
        {
            get{ return new DirectoryInfo("ops/provision"); }
        }

        protected String Separator
        {
            get{return Path.PathSeparator.ToString();}
        }
    }
}