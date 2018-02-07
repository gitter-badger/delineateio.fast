using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    /// <summary>
    /// Wrapper class around Terraform
    /// </summary>
    public class TerraformTool : HashicorpTool
    {
        public TerraformTool()
        {
            Name = "terraform";
        }

        protected override string CleanVersion(string version)
        {
            return version.Replace("Terraform v", String.Empty);
        }
    }   
}