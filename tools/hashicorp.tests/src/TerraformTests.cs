using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delineate.Fast.Tools.Hashicorp;

namespace Delineate.Fast.Tools.Tests
{
    [TestClass]
    public class TerraformTests
    {
        [TestMethod]
        public void GetActiveVersionTest()
        {   
            TerraformTool tool = new TerraformTool();

            string version = tool.GetActiveVersion();
        }

        [TestMethod]
        public void InstallTest()
        {
            TerraformTool tool = new TerraformTool();
            tool.Install("0.11.2");

            //Assert.AreEqual("0.11.3", tool.GetActiveVersion());
        } 

        [TestMethod]
        public void GetVersions()
        {
            TerraformTool tool = new TerraformTool();
            
            List<string> versions = tool.GetAllVersions();
        }
    }
}