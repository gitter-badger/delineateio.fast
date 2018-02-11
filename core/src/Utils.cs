using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;

namespace Delineate.Fast.Core
{ 
    /// <summary>
    /// Class that provides various utility methods
    /// </summary>
    public static class Utils
    {
        #region Download

        /// <summary>
        /// Downloads a file via a url
        /// </summary>
        /// <param name="url">The url tpo download from</param>
        /// <param name="file">The file to create from the download</param>
        public static void Download(Uri url, FileInfo file)
        {
            if(url == null)
                throw new ArgumentNullException("Uri was not provided");
            
            if(file ==null)
                throw new ArgumentNullException("File was not provided");

            file.Directory.Create();
            
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);
                File.WriteAllBytes(file.FullName, data);
            }
        }

        #endregion

        #region Decompress

        /// <summary>
        /// Decompresses a file to the current directory
        /// </summary>
        /// <param name="file">The file to decompress</param>
        public static void Decompress(FileInfo file)
        {
            if(file ==null)
                throw new ArgumentNullException("File was not provided");
            
            using(FileStream stream = File.OpenRead(file.FullName))
            {
                ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read);
                archive.ExtractToDirectory(file.Directory.FullName);
            }
        }

        #endregion

        #region Process Operations

        /// <summary>
        /// Runs a command using the tool
        /// </summary>
        /// <param name="args">Arguments for the tool</param>
        public static Result Run(string processName, params string[] args)
        {
            if(processName == null || processName.Length == 0)
                throw new ArgumentException("No process has been specified");

            var cmd = string.Join(" ", args);
            var escapedArgs = cmd.Replace("\"", "\\\"");

            try
            {            
                using(Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = processName,
                        Arguments = escapedArgs,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    process.Start();
                    process.WaitForExit();

                    return new Result()
                    {
                        HasError = process.ExitCode > 0,
                        Output = process.StandardOutput.ReadToEnd(),
                    };
                }
            }
            catch
            {
                return new Result()
                {
                    HasError = true,
                };
            }
        }

        #endregion

        #region File Operations

        /// <summary>
        /// Make a file executable
        /// </summary>
        /// <param name="file">File to make executable</param>
        public static void MakeExecutable(FileInfo file)
        {
            Run("chmod", "+x", file.FullName);
        }

        #endregion
    }   
}