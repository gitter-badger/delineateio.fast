using System;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Templates.Local
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandMatch(Key="local:diagnose")]
    public sealed class DiagnoseCommand : Command
    {

    }
}