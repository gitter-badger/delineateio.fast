using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Resources;
using Delineate.Fast.Core.Nodes;
using Delineate.Fast.Core.Outputs;
using Delineate.Fast.Core.Help;

namespace Delineate.Fast.Core.Commands
{
    /// <summary>
    /// Base class for all the commmands for Fast
    /// </summary>
    [CommandOption(Key="-v", Description="Returns the versions", Aliases="--version")]
    [CommandOption(Key="-h", Description="Provides help for the requested command", Aliases="--help")]
    [CommandOption(Key="-q", Description="Performs the command in quiet mode, only reporting errors", Aliases="--quiet")]
    [CommandOption(Key="-l", Description="Indicates that the command should log", Aliases="--log")]
    public abstract class Command
    {
        #region Properties

        /// <summary>
        /// Object containing info about the command
        /// </summary>
        /// <returns>The info</returns>
        public CommandInfo Info { get; set; }
        
        /// <summary>
        /// The Output manager assigned to this command 
        /// </summary>
        /// <returns></returns>
        public OutputManager Outputs = new OutputManager();

        /// <summary>
        /// Options that are av available for this command
        /// </summary>
        /// <returns>The list of options</returns>
        internal CommandOptions Options = new CommandOptions();

        /// <summary>
        /// The arguments which the command is executing with
        /// </summary>
        /// <returns>The args</returns>
        public CommandArgs Args = new CommandArgs();

        /// <summary>
        /// Log
        /// </summary>
        /// <returns></returns>
        public CommandArgsParser Parser = new CommandArgsParser();

        /// <summary>
        /// Reference to the root node of the plan
        /// </summary>
        /// <returns>The root node of the plan</returns>
        protected RootNode Root {get; set;}

        /// <summary>
        /// Indicates if apply can be safely invoked
        /// </summary>
        /// <returns>Returns true if the command can be safely applied</returns>
        protected bool IsSafe { get; set; }

        #endregion

        public Command()
        {
            //Move to factory
            Root = RootNode.Create(this);
        }

        /// <summary>
        /// Executes the current command 
        /// </summary>
        /// <param name="programArgs">The arguments that were provided to the program</param>
        public void Execute(string[] programArgs)
        {
            Parser.Parse(programArgs, Options, Args);

            Outputs.IsQuiet = false;

            Outputs.SendBlank();
            Outputs.SendImportant("Running Fast ...");


            if( Parser.HasErrors)
            {
                Outputs.SendBlank();
                Outputs.SendError("There was an error parsing the provided arguments", 1);
                DisplayHelp();
                Completed();
                return;
            }

            if( Args.IsHelp)
            {
                DisplayHelp();
                Completed();
                return;
            }

            if( Args.IsVersion)
            {
                DisplayVersion();
                Completed();
                return;
            }

            Prepare();
            Plan();

            if(IsSafe)
            { 
                Apply();
            }

            Completed();
        }

        private void Completed()
        {
            Outputs.SendBlank();
            Outputs.SendSuccess("Fast completed successfully!");
            Outputs.SendBlank();
        }

        internal void DefaultCommandWarning()
        {
            if(Info.IsDefault)
            {
                Outputs.SendBlank();
                Outputs.SendWarning("No command was found, information below if for Fast");
            }
        }

        /// <summary>
        /// Displays the help section for the current command  
        /// </summary>
        internal void DisplayHelp()
        {
            DefaultCommandWarning();
            HelpManager help = new HelpManager(this);
            help.Output();
        }

        /// <summary>
        /// Displays the versions of the engines
        /// </summary>
        internal void DisplayVersion()
        {
            DefaultCommandWarning();

            Outputs.SendBlank();
            Outputs.Indent();

            if( ! Info.IsCore)
                Outputs.SendNormal(Utils.GetPluginVersion(GetType()), false);
            
            Outputs.SendNormal(Utils.GetFastVersion(), false);
            Outputs.SendNormal(Utils.GetDotNetVersion(), false);
            Outputs.Unindent();
        }

        /// <summary>
        /// Performs any required preparation before planning the command 
        /// </summary>
        protected virtual void Prepare() {}

        /// <summary>
        /// Executes the plan for the command and shows any warnings
        /// </summary>
        protected virtual void Plan() 
        {
            Outputs.SendBlank();
            Outputs.SendNormal("Planning ...");
            Outputs.SendBlank();

            IsSafe = true;
            
            Outputs.Indent();
            Plan(Root.Nodes);
            Outputs.Unindent();

            if( ! IsSafe )
            {
                if( Args.IsForced )
                {
                    Outputs.SendBlank();
                    Outputs.SendSuccess("Warnings have been overriden");
                    IsSafe = true;
                }
                else
                {
                    Outputs.SendBlank();
                    Outputs.SendError("Commmand could not be completed, please review the warnings.", 1);
                }
            }
        }

        /// <summary>
        /// Recursive method that plans the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        /// <param name="indent">The current indent level of messages</param>
        private void Plan(List<Node> nodes, int indent = 0)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    if( node.Plan() )
                        IsSafe = false;

                    Outputs.Indent();
                    Plan(node.Nodes);
                    Outputs.Unindent();
                }
            }
        }

        /// <summary>
        /// Applies the command, only called if CanApply = true
        /// </summary>
        protected virtual void Apply()
        {   
            Outputs.SendBlank();
            Outputs.SendNormal("Applying ...");
            Outputs.SendBlank();

            Outputs.Indent();
            Apply(Root.Nodes);
            Outputs.Unindent();
        }

        /// <summary>
        /// Recursive method that applies the changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        /// <param name="indent">The current indent level of messages</param>
        private void Apply(List<Node> nodes, int indent = 0)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(ActionNode node in nodes)
                {
                    node.Apply();

                    Outputs.Indent();
                    Apply(node.Nodes, indent + 1);
                    Outputs.Unindent();
                }
            }
        }
    }
}