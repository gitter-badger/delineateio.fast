using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using Delineate.Fast.Core.Processes;

namespace Delineate.Fast.Core
{ 
    public static class Utils
    {
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

        /// <summary>
        /// Make a file executable
        /// </summary>
        /// <param name="file">File to make executable</param>
        public static void MakeExecutable(FileInfo file)
        {
            ProcessManager.Execute("chmod", "+x", file.FullName);
        }
    }   
}