using System;
using System.IO;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Messaging;


namespace Delineate.Fast.Core
{
    /// <summary>
    /// Default command is returned if there no other commands found
    /// </summary>
    [CommandInfo(Key="default", IsDefault=true, IsCore=true)]
    public sealed class DefaultCommand: Command 
    {
        protected internal override void Prepare(){}
    }
}