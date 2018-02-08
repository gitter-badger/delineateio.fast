using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delineate.Fast.Tests;
using Delineate.Fast.Core;

namespace Delineate.Fast.Utilities.Tests
{
    [TestClass]
    public class DownloadManagerTests
    {
        private const string TEST_URL = "http://ipv4.download.thinkbroadband.com/20MB.zip";

        private const string Source = "files/download";

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            TestFileHelper.InitializeFiles(Source);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            TestFileHelper.CleanupFiles(Source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoUriProvided()
        {
            string path = Path.Combine(Source, "download.zip");
            
            FileInfo file = new FileInfo(path);   
            Utils.Download(null, file);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoFileProvided()
        {
            Uri url = new Uri(TEST_URL);  

            Utils.Download(url, null);
        }

        [TestMethod]
        public void Download()
        {
            Uri url = new Uri(TEST_URL); 
            string path = Path.Combine(Source, "download.zip");

            FileInfo file = new FileInfo(path); 
            Utils.Download(url, file);
        }
    }
}
