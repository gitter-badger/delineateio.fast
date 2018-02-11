using System;
using System.Diagnostics;

using Delineate.Fast.Core.Diagnostics;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands
{
    public class ExecuteCommandHandler: BaseCommandHandler, ICommandHandler
    {
        protected override void Handle()
        {
            Command.Root = RootNode.Create(Messages);

            Command.Prepare();
            Command.Root.Debug();

            Command.Plan();
            
            if(Command.IsSafeToApply)
            { 
                Command.Apply();
            }
        }
    }
}