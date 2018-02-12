using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Collections.Generic;
using System.Diagnostics;

using Delineate.Fast.Core.Logging;
using Delineate.Fast.Core.Messaging;
using Delineate.Fast.Core.Help;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Versioning;
using Delineate.Fast.Core.Commands.Handlers;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Base class for all the Fast commmands
    /// </summary>
    [CommandOption(Key="-v", Description="Returns the relevant versions for '{0}'", Aliases="--version")]
    [CommandOption(Key="-h", Description="Provides help for the '{0}'", Aliases="--help")]
    [CommandOption(Key="-q", Description="Performs '{0}' in quiet mode, only reporting errors", Aliases="--quiet")]
    [CommandOption(Key="-l", Description="Specifies that '{0}' should be logged", Aliases="--log")]
    public abstract class Command
    {
        #region Properties

        public CommandContext Context { get; set; }

        /// <summary>
        /// Reference to the root node of the command
        /// </summary>
        /// <returns>The root node of the command</returns>
        public RootNode Root {get; set;}
        
        /// <summary>
        /// Indoicates if the 
        /// </summary>
        /// <returns></returns>
        public bool IsSafeToApply { get; set; }

        #endregion

        /// <summary>
        /// Executes the current command 
        /// </summary>
        /// <param name="programArgs">The arguments that were provided to the program</param>
        public void Execute(string[] programArgs)
        {
            Context.Logs.Log(GetType().FullName);

            //Sets the Output accordingly
            Context.Messenger.IsQuiet = false;

            // Parses the args
            Context.Args = new CommandArgs(programArgs, Context);

            //Returns the correct handler and executes
            ICommandHandler handler = GetHandler();
            handler.Execute();
            
            Context.Messenger.Flush();
        }

        /// <summary>
        /// Creates the correct handler for the request 
        /// </summary>
        /// <returns>Returns the handler to use</returns>
        private ICommandHandler GetHandler()
        {
            if(Context.Args.HasErrors)
                return CreateHandler<ArgsCommandHandler>();

            if(Context.Args.Has("-h"))
                return CreateHandler<HelpCommandHandler>();

            if(Context.Args.Has("-v"))
                return CreateHandler<VersionCommandHandler>();

            return CreateHandler<ExecuteCommandHandler>();
        }

        private T CreateHandler<T>() where T: BaseCommandHandler, new()
        {
            return new T()
            {
                Context = Context
            };
        } 


        #region Prepare

        /// <summary>
        /// Performs any required preparation before planning the command 
        /// </summary>
        protected internal virtual void Prepare() {}

        #endregion

        #region Plan

        /// <summary>
        /// Executes the plan for the command and shows any warnings
        /// </summary>
        protected internal virtual void Plan() 
        {
            
            Context.Messenger.Blank();
            Context.Messenger.Normal("Planning ...");
            Context.Messenger.Blank();

            IsSafeToApply = true;
            
            Context.Messenger.Indent();
            Context.Messenger.Nest();
            Plan(Root.Nodes);
            Context.Messenger.Unnest();
            Context.Messenger.Unindent();

            if( ! IsSafeToApply )
            {
                if( Context.Args.Has("-f"))
                {
                    Context.Messenger.Blank();
                    Context.Messenger.Success("Warnings have been overriden");
                    IsSafeToApply = true;
                }
                else
                {
                    Context.Messenger.Blank();
                    Context.Messenger.Error("Commmand could not be completed, please review the warnings.");
                }
            }
        }

        /// <summary>
        /// Recursive method that plans the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        /// <param name="indent">The current indent level of messages</param>
        private void Plan(IList<Node> nodes)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    if( node.Plan() )
                        IsSafeToApply = false;

                    Context.Messenger.Indent();
                    Plan(node.Nodes);
                    Context.Messenger.Unindent();
                }
            }
        }

        #endregion

        #region Apply

        /// <summary>
        /// Applies the command, only called if CanApply = true
        /// </summary>
        protected internal virtual void Apply()
        {   
            Context.Messenger.Blank();
            Context.Messenger.Normal("Applying ...");
            Context.Messenger.Blank();

            Context.Messenger.Indent();
            Context.Messenger.Nest();
            Apply(Root.Nodes);
            Context.Messenger.Unnest();
            Context.Messenger.Unindent();
        }

        /// <summary>
        /// Recursive method that applies the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        private void Apply(IList<Node> nodes)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    node.Apply();

                    Context.Messenger.Indent();
                    Apply(node.Nodes);
                    Context.Messenger.Unindent();
                }
            }
        }

        #endregion
    }
}