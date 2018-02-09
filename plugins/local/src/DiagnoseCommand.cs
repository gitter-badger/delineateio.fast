using System;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Plugins.Local
{
    /// <summary>
    /// Setup command that is used to create the project artefacts 
    /// </summary>
    [CommandInfo(Key="diagnose:local", Description="Diagnoses the local environment", PluginName="local")]
    public sealed class DiagnoseCommand : Command
    {
        
    }
}