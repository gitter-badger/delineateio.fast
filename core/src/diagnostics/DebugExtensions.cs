using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace Delineate.Fast.Core.Diagnostics
{
    /// <summary>
    /// Extensions methods that can be 
    /// </summary>
    public static class DebugExtensions
    {
        /// <summary>
        /// Extension method that can print serialised objects to the 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string Debug(this IDebuggable instance)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(instance.GetType());

            using (MemoryStream stream = new MemoryStream())
            {  
                serializer.WriteObject(stream, instance);  
                stream.Position = 0;  

                using (StreamReader reader = new StreamReader(stream))
                {  
                    return reader.ReadToEnd();
                }
            }
        }   
    }
}