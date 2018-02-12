using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace Delineate.Fast.Core.Logging
{
    /// <summary>
    /// Extensions methods that can be 
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Extension method that can print serialised objects to the 
        /// </summary>
        /// <param name="instance">The instance to be converted to Json</param>
        /// <returns></returns>
        public static string ToLog(this ILoggable instance)
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