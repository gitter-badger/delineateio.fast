using System;
using System.IO;

namespace Delineate.Fast.Tests
{
    public static class TestFileHelper
    { 
        /// <summary>
        /// Creates the test files are required
        /// </summary>
        /// <param name="source"></param>
        public static void InitializeFiles(string source)
        {
            if(Directory.Exists(source))
                CleanupFiles(source);

            DirectoryInfo directory = Directory.CreateDirectory(source);

            DirectoryInfo testFiles = new DirectoryInfo("../../../" + source);

            if(testFiles.Exists)
            {
                foreach(FileInfo file in testFiles.GetFiles())
                {
                    File.Copy(file.FullName, Path.Combine(directory.FullName, file.Name));
                }
            }
        }

        /// <summary>
        /// Cleans up the test files 
        /// </summary>
        /// <param name="source">The source to remove</param>
        public static void CleanupFiles(string source)
        {
            Directory.Delete(source, true);
        }
    }
}