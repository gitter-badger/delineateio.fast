using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Delineate.Fast;
using Delineate.Fast.Utilities;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    /// <summary>
    /// Hashicorp Helper for utility methods that are
    /// cvommon between the hashicorp products
    /// </summary>
    public abstract class HashicorpTool: Tool
    {

        public override string GetActiveVersion()
        {
            ToolResult result = Run("--version");

            if(result.HasError)
                throw new ApplicationException(result.StandardError);

            return CleanVersion(result.StandardOutput);
        }

        /// <summary>
        /// Tool specific cleaning of the version response 
        /// </summary>
        /// <param name="version">The raw version returned from the tool</param>
        /// <returns>Returns a cleaned version</returns>
        protected abstract string CleanVersion(string version);

        public override List<string> GetAllVersions()
        {
            string home = Environment.GetEnvironmentVariable("HOME");
            string softwarePath = Path.Combine(home,
                                        "software",
                                        Name
                                        ); 

            List<string> versions = new List<string>();

            if(! Directory.Exists(softwarePath))
                return versions;
            
            DirectoryInfo directory = new DirectoryInfo(softwarePath);

            foreach(DirectoryInfo versionDirectory in directory.GetDirectories())
            {
                versions.Add(versionDirectory.Name);
            }

            return versions;
        } 

        /// <summary>
        /// Installs the hashicorp tool in the environment
        /// </summary>
        /// <param name="toolName">Name of the tool to install</param>
        /// <param name="version">Version of the tool, to use</param>
        public override void Install(string version)
        {
            if(version == null)
                throw new ArgumentNullException();

            if(version.Length == 0)
                throw new ArgumentException();
            
            FileInfo compressedFile = Download(Name, version);
            
            FileInfo decompressedFile = Decompress(Name, compressedFile);

            Move(Name, version, decompressedFile);

            SwitchTo(version);
        }
        
        #region Install Methods

        /// <summary>
        /// Performs the download for the required product and version
        /// </summary>
        /// <param name="name">The name of the Hashicorp product</param>
        /// <param name="version">The version of the hashicorp product</param>
        /// <returns>Reference to the downloaded file</returns>
        private FileInfo Download(string name, string version)
        {
            string compressedFileName = string.Format("{0}_{1}_darwin_amd64.zip",
                                            name,
                                            version);

            Uri url = new Uri(string.Format("https://releases.hashicorp.com/{0}/{1}/{2}",
                                            name,
                                            version,
                                            compressedFileName));
            
            FileInfo compressedFile = new FileInfo(Path.Combine(
                                                        Path.GetTempPath(), 
                                                        compressedFileName));
            
            DownloadManager.Download(url, compressedFile);

            return compressedFile;
        }

        /// <summary>
        /// Decompresses the downloaded file
        /// </summary>
        /// <param name="name">The name of the product being downloaded</param>
        /// <param name="compressedFile">The reference to the the file to be decompressed</param>
        /// <returns>Reference to the decompressed file</returns>
        private FileInfo Decompress(string name, FileInfo compressedFile)
        {
            // Ensures no clash in temp dir
            if(File.Exists(Path.Combine(compressedFile.Directory.FullName, name)))
                File.Delete(Path.Combine(compressedFile.Directory.FullName, name));

            CompressionManager.Decompress(compressedFile);

            string decompressedFileName = Path.Combine(
                                            compressedFile.Directory.FullName,
                                            name);

             return new FileInfo(decompressedFileName);
        }

        /// <summary>
        /// Moves to a location that will be referenced on the $PATH
        /// </summary>
        /// <param name="name">The name of product being installed </param>
        /// <param name="version">Version of the product being installed</param>
        /// <param name="decompressedFile">The decompressed binary</param>
        private void Move(string name, string version, FileInfo decompressedFile)
        {
            string home = Environment.GetEnvironmentVariable("HOME");
            string softwarePath = Path.Combine(home,
                                        "software",
                                        name,
                                        version
                                        ); 

            if(Directory.Exists(softwarePath))
                Directory.Delete(softwarePath, true);
            
            Directory.CreateDirectory(softwarePath);

            decompressedFile.MoveTo( Path.Combine(softwarePath, decompressedFile.Name));
        }

        #endregion

        /// <summary>
        /// Switching of the version of the product 
        /// </summary>
        public override void SwitchTo(string version)
        {
            FileInfo binFile = new FileInfo(Path.Combine("/usr/local/bin", Name));

            if(binFile.Exists)
               binFile.Delete();

            File.Copy(Path.Combine("/software", Name, version, Name), binFile.FullName);
        }

        public override void Uninstall(string version)
        {
            FileInfo binFile = new FileInfo(Path.Combine("/usr/local/bin", Name));

            if(GetActiveVersion() == version)
                binFile.Delete();
        }
    }
}