using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Delineate.Fast.Core.Diagnostics;

namespace Delineate.Fast.Core.Messages
{
    /// <summary>
    /// The Command message events args 
    /// </summary>
    [DataContract]
    [KnownType(typeof(MessageInfo))]
    public class MessageEventArgs: EventArgs, IDebuggable
    {
        /// <summary>
        /// Lines of the text 
        /// </summary>
        /// <returns>Returns the list of lines</returns>
        [DataMember]
        public IList<MessageInfo> Messages { get; set; }
    } 
}
