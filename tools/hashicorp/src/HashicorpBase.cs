using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Delineate.Fast;
using Delineate.Fast.Core;
using Delineate.Fast.Core.Tools;

namespace Delineate.Fast.Tools.Hashicorp
{ 
    /// <summary>
    /// Base class for the Hashicorp tools
    /// </summary>
    public abstract class HashicorpBase: Tool
    {   

        public HashicorpBase(string name, string version) : base (name, version) {}

         public string GetRawVersion()
        {
            ToolResult result = Run("--version");
            return result.Output;
        }
    
        /// <summary>
        /// Retrieves an ordered list of the versions
        /// </summary>
        /// <returns>Returns the collection of versions</returns>
        /// <remarks>
        /// Returns a list of zero items if no versions are installed
        /// </remarks>
        public override IList<string> GetAllVersions()
        {
            IList<string> versions = new List<string>();
            DirectoryInfo tool = GetToolDirectory();

            if(tool.Exists)
            {
                foreach(DirectoryInfo version in tool.GetDirectories())
                {
                    versions.Add(version.Name);
                }
            }

            return versions;
        } 

        /// <summary>
        /// Installs the hashicorp tool in the environment
        /// </summary>
        /// <param name="activate">Switch to this version after installation</param>
        public override void Install(bool activate = false)
        {            
            // Downloads from Hashicorp
            FileInfo compressedFile = Download();
            
            //Decompressed the file 
            FileInfo decompressedFile = Decompress(compressedFile);

            // Moves the file to the permanent 
            Move(decompressedFile);

            if(activate)
                Activate();
        }
        
        #region Install Methods

        /// <summary>
        /// Performs the download for the required product and version
        /// </summary>
        /// <param name="name">The name of the Hashicorp product</param>
        /// <param name="version">The version of the hashicorp product</param>
        /// <returns>Reference to the downloaded file</returns>
        private FileInfo Download()
        {
            string compressedFileName = string.Format("{0}_{1}_darwin_amd64.zip",
                                            Name,
                                            Version);

            Uri url = new Uri(string.Format("https://releases.hashicorp.com/{0}/{1}/{2}",
                                            Name,
                                            Version,
                                            compressedFileName));
            
            FileInfo compressedFile = new FileInfo(Path.Combine(
                                                        Path.GetTempPath(), 
                                                        compressedFileName));
            
            Utils.Download(url, compressedFile);

            return compressedFile;
        }

        /// <summary>
        /// Decompresses the downloaded file
        /// </summary>
        /// <param name="name">The name of the product being downloaded</param>
        /// <param name="compressedFile">The reference to the the file to be decompressed</param>
        /// <returns>Reference to the decompressed file</returns>
        private FileInfo Decompress(FileInfo compressedFile)
        {
            // Ensures no clash in temp dir
            if(File.Exists(Path.Combine(compressedFile.Directory.FullName, Name)))
                File.Delete(Path.Combine(compressedFile.Directory.FullName, Name));

            Utils.Decompress(compressedFile);

            string decompressedFileName = Path.Combine(
                                            compressedFile.Directory.FullName,
                                            Name);

             return new FileInfo(decompressedFileName);
        }

        /// <summary>
        /// Moves to a location that will be referenced on the $PATH
        /// </summary>
        /// <param name="name">The name of product being installed </param>
        /// <param name="version">Version of the product being installed</param>
        /// <param name="decompressedFile">The decompressed binary</param>
        private void Move(FileInfo decompressedFile)
        {
            string home = Environment.GetEnvironmentVariable("HOME");
            string softwarePath = Path.Combine(home, "software", Name, Version); 

            if(Directory.Exists(softwarePath))
                Directory.Delete(softwarePath, true);
            
            Directory.CreateDirectory(softwarePath);

            decompressedFile.MoveTo( Path.Combine(softwarePath, decompressedFile.Name));

            Utils.MakeExecutable(new FileInfo(Path.Combine(softwarePath, decompressedFile.Name)));
        }

        #endregion

        /// <summary>
        /// Switches to the version of the product 
        /// </summary>
        public override void Activate()
        {
            File.Copy(GetVersionFile().FullName, GetExecutableFile().FullName, true);
        }

        /// <summary>
        /// Uninstalls the specific version of the tool
        /// </summary>
        /// <remarks>
        /// If the active version then this will need to be reset
        /// </remarks>
        public override void Uninstall()
        {
            if(Version == GetActiveVersion())
                GetExecutableFile().Delete();
        
            DirectoryInfo versionDirectory = GetVersionDirectory();

            if(versionDirectory.Exists)
                Directory.Delete(versionDirectory.FullName, true);
        }

        /// <summary>
        /// Uninstalls all version of the tool from the machine 
        /// </summary>
        public override void UninstallAll() 
        {
            FileInfo executableFile = GetExecutableFile();

            if(executableFile.Exists)
                executableFile.Delete();
        
            DirectoryInfo toolDirectory = GetToolDirectory();

            if(toolDirectory.Exists)
                Directory.Delete(toolDirectory.FullName, true);
        }

        /// <summary>
        /// Gets a reference to the version file
        /// </summary>
        /// <returns>The FileInfo reference</returns>
        protected FileInfo GetVersionFile()
        {
            string path = Path.Combine(GetVersionDirectory().FullName, Name);
            return new FileInfo(path);
        }

        /// <summary>
        /// Gets a reference to the executable file
        /// </summary>
        /// <returns>The FileInfo reference</returns>
        protected FileInfo GetExecutableFile()
        {
            string path = Path.Combine(GetExecutableDirectory().FullName, Name);
            return new FileInfo(path);
        }
    }
}