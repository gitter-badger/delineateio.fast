using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Delineate.Fast.Core.Logging;

namespace Delineate.Fast.Core.Messaging
{
    /// <summary>
    /// The Command message events args 
    /// </summary>
    [DataContract]
    public sealed class MessageInfo : ILoggable
    {
        /// <summary>
        /// Lines of the text 
        /// </summary>
        /// <returns>Returns the list of lines</returns>
        [DataMember]
        public string Text { get; set; }

        /// <summary>
        /// The color to be used when writing out the text
        /// </summary>
        /// <returns></returns>
        [DataMember]
        public MessageLevel Level { get; set; }

        /// <summary>
        /// The indent level to use
        /// </summary>
        /// <returns>Returns the indent value</returns>
        [DataMember]
        public int Indent { get; set; }

        /// <summary>
        /// Indicates if indent is nested and formats 
        /// </summary>
        /// <returns>Returns true if nested</returns>
        [DataMember]
        public bool IsNested { get; set; }
    } 
}
