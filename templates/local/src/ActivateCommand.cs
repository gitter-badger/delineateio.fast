using System;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Projects.Local.Commands
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="local:show")]
    [CommandOption()]
    public sealed class ShowCommand : Command
    {

    }
}