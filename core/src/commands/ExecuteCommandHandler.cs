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
            Context.Command.Root = RootNode.Create(Context);

            Context.Command.Prepare();
            Context.Command.Root.Debug();

            Context.Command.Plan();
            
            if(Context.Command.IsSafeToApply)
            { 
                Context.Command.Apply();
            }
        }
    }
}