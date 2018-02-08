using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    /// <summary>
    /// Wrapper class around Terraform
    /// </summary>
    //[ToolName(Name="terraform")]
    public class Terraform : HashicorpBase
    {

        /// <summary>
        /// Constructor for specificing the version of Terraform to 
        /// work with via this reference 
        /// </summary>
        /// <param name="version">The version to use</param>
        /// <remarks>
        /// If the version is not provided then default is the current stable
        /// version, which is currently 0.11.3
        /// </remarks>
        public Terraform(string version = "0.11.3") : base ("terraform", version) {}

        public override string GetActiveVersion()
        {
            string raw = GetRawVersion();

            if(raw == null) return null;
            
            string response = raw.Replace("Terraform v", String.Empty);

            string[] lines = response.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None);

            return lines[0];
        }
    }   
}