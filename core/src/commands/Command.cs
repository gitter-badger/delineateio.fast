using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Collections.Generic;
using System.Diagnostics;

using Delineate.Fast.Core.Diagnostics;
using Delineate.Fast.Core.Messages;
using Delineate.Fast.Core.Help;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Versioning;

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

        /// <summary>
        /// The command info for this command
        /// </summary>
        /// <returns>The info</returns>
        public CommandInfo Info { get; set; }
        
        /// <summary>
        /// The Output manager assigned to this command 
        /// </summary>
        /// <returns>Class that provides the outputting functionality</returns>
        public MessageManager Outputs = new MessageManager();

        /// <summary>
        /// Options that are available for this command
        /// </summary>
        /// <returns>The list of options</returns>
        public CommandOptions Options = new CommandOptions();

        /// <summary>
        /// The arguments which the command is currently executing with
        /// </summary>
        /// <returns>The args</returns>
        public CommandArgs Args {get; set;}

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
            Debug.Indent();
            Debug.WriteLine("Executing command {0} ...", GetType().FullName);

            //Sets the Output accordingly
            Outputs.IsQuiet = false;

            // Parses the args
            Args = new CommandArgs(programArgs, Options);

            //Returns the correct handler and executes
            ICommandHandler handler = GetHandler();
            handler.Execute();
            Outputs.Flush();
        }

        /// <summary>
        /// Creates the correct handler for the request 
        /// </summary>
        /// <returns>Returns the handler to use</returns>
        private ICommandHandler GetHandler()
        {
            if(Args.HasErrors)
                return CreateHandler<ArgsCommandHandler>();

            if(Args.Has("-h"))
                return CreateHandler<HelpCommandHandler>();

            if(Args.Has("-v"))
                return CreateHandler<VersionCommandHandler>();

            return CreateHandler<ExecuteCommandHandler>();
        }

        private T CreateHandler<T>() where T: BaseCommandHandler, new()
        {
            return new T()
            {
                Command = this,
                Messages = this.Outputs
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
            Outputs.Blank();
            Outputs.Normal("Planning ...");
            Outputs.Blank();

            IsSafeToApply = true;
            
            Outputs.Indent();
            Outputs.Nest();
            Plan(Root.Nodes);
            Outputs.Unnest();
            Outputs.Unindent();

            if( ! IsSafeToApply )
            {
                if( Args.Has("-f"))
                {
                    Outputs.Blank();
                    Outputs.Success("Warnings have been overriden");
                    IsSafeToApply = true;
                }
                else
                {
                    Outputs.Blank();
                    Outputs.Error("Commmand could not be completed, please review the warnings.");
                }
            }
        }

        /// <summary>
        /// Recursive method that plans the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        /// <param name="indent">The current indent level of messages</param>
        private void Plan(List<Node> nodes)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    if( node.Plan() )
                        IsSafeToApply = false;

                    Outputs.Indent();
                    Plan(node.Nodes);
                    Outputs.Unindent();
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
            Outputs.Blank();
            Outputs.Normal("Applying ...");
            Outputs.Blank();

            Outputs.Indent();
            Outputs.Nest();
            Apply(Root.Nodes);
            Outputs.Unnest();
            Outputs.Unindent();
        }

        /// <summary>
        /// Recursive method that applies the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        private void Apply(List<Node> nodes)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    node.Apply();

                    Outputs.Indent();
                    Apply(node.Nodes);
                    Outputs.Unindent();
                }
            }
        }

        #endregion
    }
}