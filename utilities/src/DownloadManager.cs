using System;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Delineate.Fast.Utilities
{ 
    public static class DownloadManager
    {
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
    }   
}