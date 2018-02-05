using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Resources;
using Delineate.Fast.Nodes;

namespace Delineate.Fast.Commands
{
    /// <summary>
    /// Base class for all the commmands for Fast
    /// </summary>
    public abstract class Command
    {
        #region Properties

        /// <summary>
        /// The configuration parameters 
        /// </summary>
        /// <returns>Array of the config values</returns>
        protected string[] Parameters = new []{"circleci", "github", "docker", "packer", "terraform"};

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected CommandOptions Options = new CommandOptions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected CommandArgs Args = new CommandArgs();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CommandArgsParser Parser = new CommandArgsParser();

        /// <summary>
        /// Reference to the root node of the plan
        /// </summary>
        /// <returns>The root node of the plan</returns>
        protected RootNode Root = RootNode.Create();

        /// <summary>
        /// Indicates if apply can be safely invoked
        /// </summary>
        /// <returns>Returns true if the command can be safely applied</returns>
        protected bool CanApply { get; set; }

        #endregion

        /// <summary>
        /// Executes the current command 
        /// </summary>
        /// <param name="programArgs">The arguments that were provided to the program</param>
        public void Execute(string[] programArgs)
        { 
            AddOptions(); // Add command options
            Parser.Parse(programArgs, Options, Args);

            if( Parser.HasErrors)
            {
                Help();
            }
            else
            {
                Prepare();
                Plan();
                if(CanApply)
                { 
                    Apply();
                }
            }
        }

        /// <summary>
        /// Displays the help section for the current command  
        /// </summary>
        private void Help()
        {
            //TODO: Implement the help display    
        }

        /// <summary>
        /// Can be overriden to add command specific options
        /// </summary>
        protected virtual void AddOptions() { }


        /// <summary>
        /// Helper method to indicate if a specific parameter is set 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool HasParameter(string key)
        {
            return Parameters.Contains(key);
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
            List<string> warnings = new List<string>();

            foreach(Node node in Root.Nodes)
            {   
                node.Plan(warnings);
            }

            if(warnings.Count > 0)
            {
                ConsoleWriter.WriteLine("Planning ...");
                ConsoleWriter.WriteLines(warnings, ConsoleColor.Yellow, 1, true);

                if(Args.IsForced)
                {
                    ConsoleWriter.WriteLine("Warnings have been overriden", blank: 1);
                    CanApply = true;
                }    
                else
                {
                    ConsoleWriter.WriteLine("Commmand could not be completed.  Please review the warnings.", ConsoleColor.Red, blank: 1);
                }
            }
            else
            {
                CanApply = true;
            }
        }

        /// <summary>
        /// Applies the command, only called if CanApply = true
        /// </summary>
        protected virtual void Apply()
        {
            ConsoleWriter.WriteLine("Applying ...", blank: 1);   
            Apply(Root.Nodes);
        }

        /// <summary>
        /// Recursive method that performs the planned changes
        /// </summary>
        /// <param name="nodes">Nodes to perform the action for</param>
        /// <param name="indent">The current indent level of messages</param>
        private void Apply(List<Node> nodes, int indent = 1)
        {
            if(nodes != null && nodes.Count > 0)
            {
                foreach(Node node in nodes)
                {
                    node.Apply();
                    Apply(node.Nodes, indent + 1);
                }
            }
        }
    }
}