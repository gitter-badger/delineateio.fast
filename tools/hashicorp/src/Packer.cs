using System;
using System.Collections.Generic;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    /// <summary>
    /// Class that represents the Hashicorp Packer product
    /// </summary>
    public class Packer: HashicorpBase
    {   
        /// <summary>
        /// Constructor for specificing the version of packer to 
        /// work with via this reference 
        /// </summary>
        /// <param name="version">The version to use</param>
        /// <returns></returns>
        /// <remarks>
        /// If the version is not provided then default is the current stable
        /// version, which is currently 1.1.3
        /// </remarks>
        public Packer(string version = "1.1.3") : base ("packer", version) {}

        public override string GetActiveVersion()
        {
            string raw = GetRawVersion();

            return raw.Replace(Environment.NewLine, String.Empty);
        }
    }   
}