using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delineate.Fast.Utilities;
using Delineate.Fast.Tests;

namespace Delineate.Fast.Utilities.Tests
{
    [TestClass]
    public class CompressionTests
    {
        private const string Source = "files/compression";

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
        public void NoFileProvided()
        { 
            CompressionManager.Decompress(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileDoesNotExist()
        {
            FileInfo file = new FileInfo(Path.Combine(Source, "none.zip"));
 
            CompressionManager.Decompress(file);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void InvalidFormatFile()
        {
            FileInfo file = new FileInfo(Path.Combine(Source, "not_ok.zip"));

            CompressionManager.Decompress(file);
        }

        [TestMethod]
        public void Decompress()
        {   
            FileInfo file = new FileInfo(Path.Combine(Source, "ok.zip"));
            CompressionManager.Decompress(file);
        }
    }
}
