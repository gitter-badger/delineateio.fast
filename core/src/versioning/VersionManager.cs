using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;

namespace Delineate.Fast.Core.Versioning
{ 
    /// <summary>
    /// Class that reqturns versions
    /// </summary>
    public static class VersionManager
    {
        /// <summary>
        /// Gets a particular plugin version
        /// </summary>
        /// <param name="type">The type to use to return the version</param>
        /// <returns>Version value</returns>
        public static string GetPluginVersion(Type type)
        {
            string version = type.Assembly.GetName().Version.ToString();
            return "Plugin".PadRight(12) + version;
        }

        /// <summary>
        /// Gets the version of the Fast framework
        /// </summary>
        /// <returns>Version value</returns>
        public static string GetFastVersion()
        {
            string version = typeof(VersionManager).Assembly.GetName().Version.ToString();
            return "Fast Engine".PadRight(12) + version;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDotNetVersion()
        {
            string version = Utils.Run("dotnet", "--version").Output;
            version = version.Replace(Environment.NewLine, string.Empty);
            return ".NET Core".PadRight(12) + version;
        }
    }   
}