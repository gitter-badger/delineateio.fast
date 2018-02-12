using System;
using System.Diagnostics;

using Delineate.Fast.Core.Logging;
using Delineate.Fast.Core.Nodes;

namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// Standard handler for executing a command
    /// </summary>
    public class ExecuteCommandHandler: BaseCommandHandler, ICommandHandler
    {
        /// <summary>
        /// Handles the command execution with three key
        /// steps: p[repare, Plan and Apply]
        /// </summary>
        protected override void Handle()
        {
            Context.Command.Root = RootNode.Create(Context);

            Context.Command.Prepare();
            Context.Command.Root.ToLog();

            Context.Command.Plan();
            
            if(Context.Command.IsSafeToApply)
            { 
                Context.Command.Apply();
            }
        }
    }
}