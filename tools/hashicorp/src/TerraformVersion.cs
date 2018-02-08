using System;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{
    /// <summary>
    /// Encapulsates the results of the tool
    /// </summary>
    public class TerraformVersion : ToolVersion
    {
        /// <summary>
        /// Required to use TerraformVersion as T
        /// </summary>
        public TerraformVersion() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public TerraformVersion(string raw) : base (raw) {} 

        /// <summary>
        /// 
        /// </summary>
        protected override void Transform()
        {
            string response = Raw.Replace("Terraform v", String.Empty);

            string[] lines = response.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None);

            Version = lines[0];
        }
    }
}