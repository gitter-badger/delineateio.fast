using System; 
using System.IO;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delineate.Fast.Core.Tools;
using Delineate.Fast.Tools.Hashicorp;

namespace Delineate.Fast.Tools.Tests
{
    [TestClass]
    public class TerraformTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Terraform terraform = new Terraform();
            terraform.UninstallAll();
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void InvalidVersionTest()
        {   
            Terraform tool = new Terraform("2.0.0");
            tool.Install();
        }

        [TestMethod]
        public void NoInstallTest()
        {   
            Terraform tool = new Terraform();
            string version = tool.GetActiveVersion();

            Assert.IsNull(version);
        }

        [TestMethod]
        public void InstallOneWithActivateTest()
        {   
            Terraform tool = new Terraform("0.11.0");
            tool.Install(true);
            
            Assert.AreEqual("0.11.0", tool.GetActiveVersion());
        }

        [TestMethod]
        public void InstallTwoWithActivateTest()
        {   
            Terraform firstTool = new Terraform("0.11.0");
            firstTool.Install(true);

            Terraform secondTool = new Terraform("0.11.1");
            secondTool.Install(true);

            Assert.AreEqual("0.11.1", firstTool.GetActiveVersion());
            Assert.AreEqual("0.11.1", secondTool.GetActiveVersion());
        }

        [TestMethod]
        public void ReinstallTest()
        {   
            Terraform tool = new Terraform("0.11.1");
            tool.Install(true);
            tool.Install(true);

            string version = tool.GetRawVersion();
                
            Assert.AreEqual("0.11.1", tool.GetActiveVersion());
        }

        [TestMethod]
        public void InstallWithoutActivateTest()
        {   
            Terraform firstTool = new Terraform("0.11.0");
            firstTool.Install(true);

            Terraform secondTool = new Terraform("0.11.1");
            secondTool.Install();

            Assert.AreEqual("0.11.0", firstTool.GetActiveVersion());
            Assert.AreEqual("0.11.0", secondTool.GetActiveVersion());
        }

        [TestMethod] 
        public void ActivateToPreviosVersionTest()
        {
            Terraform firstTool = new Terraform("0.11.0");
            firstTool.Install(true);

            Terraform secondTool = new Terraform("0.11.1");
            secondTool.Install(true);

            firstTool.Activate();

            Assert.AreEqual("0.11.0", firstTool.GetActiveVersion());
            Assert.AreEqual("0.11.0", secondTool.GetActiveVersion());
        }

        [TestMethod]
        public void GetVersions()
        {
            string[] versionInstalls = new string[]{ "0.11.0", "0.11.1", "0.11.2", "0.11.3"} ;

            foreach(string versionInstall in versionInstalls)
            {
                Terraform tool = new Terraform(versionInstall);
                tool.Install();
            }

            Terraform testTool = new Terraform();
            Assert.AreEqual(versionInstalls.Length, testTool.AllVersions.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Terraform terraform = new Terraform();
            terraform.UninstallAll();
        }
    }
}