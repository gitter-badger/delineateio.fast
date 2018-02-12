using System;
using System.Diagnostics;

using Delineate.Fast.Core.Messaging;

namespace Delineate.Fast.Core.Commands.Handlers
{
    /// <summary>
    /// The Base class of all the assigned handlers
    /// that can be processed for commands 
    /// </summary>
    public abstract class BaseCommandHandler: ICommandHandler
    {
        /// <summary>
        /// A reference to the command being executed 
        /// </summary>
        /// <returns></returns>
        public CommandContext Context { get;set; }
        
        /// <summary>
        /// Executes the handler and top and tails with the 
        /// required messaiges  
        /// </summary>
        public void Execute()
        {
            if(Context.Info.IsDefault)
            {
                Context.Messenger.Blank();
                Context.Messenger.Warning("No command was found, information below if for Fast");
            }

            Context.Messenger.Blank();
            Context.Messenger.Important("Running Fast ...");

            Context.Logs.Log(this.GetType().FullName);

            Handle();

            Context.Messenger.Blank();
            Context.Messenger.Success("Fast completed successfully!");
            Context.Messenger.Blank();
        }
        
        /// <summary>
        /// The method to be overriden by the handler implementation
        /// classes with the required functionality
        /// </summary>
        protected abstract void Handle();
    }
}