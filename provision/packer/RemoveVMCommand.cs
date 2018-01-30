using System;
using System.IO;
using Delineate.Fast;

namespace Delineate.Fast.Provision.Packer
{
    [CommandMatch(new String[]{"vm", "remove"} )]
    public class RemovePackerVmCommand : ProvisionCommand
    {
        protected override void Apply(CommandArgs args)
        {

        }    
    }
}