using System;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{
    /// <summary>
    /// Encapulsates the results of the tool
    /// </summary>
    public class PackerVersion : ToolVersion
    {
        /// <summary>
        /// Required to be used as T
        /// </summary>
        public PackerVersion(){}

        public PackerVersion(string raw) : base (raw) {} 

        protected override void Transform()
        {
            Version = Raw.Replace(Environment.NewLine, String.Empty);
        }
    }
}