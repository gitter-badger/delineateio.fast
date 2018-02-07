using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    public class PackerTool: HashicorpTool
    {
        public PackerTool()
        {
            Name = "packer";
        }
        
        protected override string CleanVersion(string version)
        {
            return version.Replace(Environment.NewLine, String.Empty);
        }
    }   
}