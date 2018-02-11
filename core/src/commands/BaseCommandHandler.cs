using System;
using System.Diagnostics;

using Delineate.Fast.Core.Messages;

namespace Delineate.Fast.Core.Commands
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
        public Command Command { get;set; }

        public MessageManager Messages { get;set; }
        
        /// <summary>
        /// Executes the handler and top and tails with the 
        /// required messaiges  
        /// </summary>
        public void Execute()
        {
            if(Command.Info.IsDefault)
            {
                Messages.Blank();
                Messages.Warning("No command was found, information below if for Fast");
            }


            Messages.Blank();
            Messages.Important("Running Fast ...");

            Debug.Indent();
            Debug.WriteLine(this.GetType().FullName);

            Handle();

            Debug.Unindent();

            Messages.Blank();
            Messages.Success("Fast completed successfully!");
            Messages.Blank();
        }
        
        /// <summary>
        /// The method to be overriden by the handler implementation
        /// classes with the required functionality
        /// </summary>
        protected abstract void Handle();
    }
}