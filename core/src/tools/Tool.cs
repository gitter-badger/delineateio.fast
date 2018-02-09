using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delineate.Fast.Core.Tools
{
    /// <summary>
    /// The base class for all tool implementations
    /// </summary>
    public abstract class Tool
    {
        #region Properties

        /// <summary>
        /// The name of the tool to use
        /// </summary>
        /// <returns></returns>
        protected string Name { get; set; }

        /// <summary>
        /// The version of the tool to use
        /// </summary>
        /// <returns></returns>
        public string Version { get; set; }

        public List<string> AllVersions
        {
            get
            {
                return GetAllVersions();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor that all tools must confirm to
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public Tool(string name, string version)
        {   
            //TODO : Additional harding required for Name parameter 

            if(version == null)
                throw new ArgumentNullException();

            if(version.Length == 0)
                throw new ArgumentException();

            Name = name;
            Version = version;
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Gets the active version for the tool
        /// </summary>
        /// <returns>Returns the version in the tools format</returns>
        public abstract string GetActiveVersion();

        /// <summary>
        /// Returns a sorted list of installed versions
        /// </summary>
        /// <returns>The list of installed versions</returns>
        public abstract List<string> GetAllVersions(); 

        /// <summary>
        /// Performs the install of the tool
        /// </summary>
        /// <param name="activate">Switch to this version after installation</param>
        public abstract void Install(bool activate = false);

        /// <summary>
        /// Switches to a particular version 
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Uninstalls the specified version of the product
        /// </summary>
        public abstract void Uninstall();

        /// <summary>
        /// Uninstalls all version of the tool
        /// </summary>
        public abstract void UninstallAll();

        #endregion

        #region Run Method

        /// <summary>
        /// Runs a command using the tool
        /// </summary>
        /// <param name="args">Arguments for the tool</param>
        protected ToolResult Run(params string[] args)
        {
            return new ToolResult( Utils.Run(Name, args) );
        }

        #endregion

        #region Directory Methods

        /// <summary>
        /// Returns the software path for the machine where runnning
        /// </summary>
        /// <returns>The software name where running</returns>
        protected DirectoryInfo GetSoftwareDirectory()
        {
            //TODO: Need to make sure this platform aware

            string home = Environment.GetEnvironmentVariable("HOME");
            return new DirectoryInfo(Path.Combine(home, "software")); 
        }

        /// <summary>
        /// Returns the directory for a specific tool
        /// </summary>
        /// <returns>Returns a reference to the doirectory</returns> 
        protected DirectoryInfo GetToolDirectory()
        {
            string path = Path.Combine(GetSoftwareDirectory().FullName, Name);
            return new DirectoryInfo(path); 
        }

        /// <summary>
        /// Returns the directory where the version is stored
        /// </summary>
        /// <returns>Returns a reference to the doirectory</returns>        
        protected DirectoryInfo GetVersionDirectory()
        {
            string path = Path.Combine(GetToolDirectory().FullName, Version);
            return new DirectoryInfo(path); 
        }

        /// <summary>
        /// Returns the directory where the executable is required 
        /// to be to run - differes by platform
        /// </summary>
        /// <returns></returns>        
        protected DirectoryInfo GetExecutableDirectory()
        {
            //TODO : Make this platform aware
            return new DirectoryInfo("/usr/local/bin");
        }

        #endregion
    }
}