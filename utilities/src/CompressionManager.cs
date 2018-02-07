using System;
using System.IO;
using System.IO.Compression;

namespace Delineate.Fast.Utilities
{ 
    public static class CompressionManager
    {
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
    }   
}