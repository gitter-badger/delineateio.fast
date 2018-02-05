using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Resources;
using Delineate.Fast.Nodes;

namespace Delineate.Fast.Commands
{
    public delegate void OutputEventHandler(object sender, OutputEventArgs e);

    /// <summary>
    /// Base class for all the commmands for Fast
    /// </summary>
    public abstract class Command
    {
        public event OutputEventHandler OnOutput;

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
        protected RootNode Root {get; set;}


        /// <summary>
        /// Indicates if apply can be safely invoked
        /// </summary>
        /// <returns>Returns true if the command can be safely applied</returns>
        protected bool CanApply { get; set; }

        #endregion

        public Command()
        {
            Root = RootNode.Create(this);
        }

        public void Output(string line, ConsoleColor color = ConsoleColor.White, 
                                            int blanks = 0, int indent = 0)
        {
            
            List<string> lines = new List<string>();
            lines.Add(line);
            Output(lines, color, blanks, indent);
        }

        public void Output(IList<string> lines, ConsoleColor color = ConsoleColor.White, 
                                            int blanks = 0, int indent = 0)
        {

            OutputEventHandler handler = OnOutput; 
            OutputEventArgs e = new OutputEventArgs()
            {
                Lines = lines,
                Color = color,
                Blanks = blanks,
                Indent = indent 
            };
                
            if (handler != null) 
            { 
                // Invokes the delegates. 
                handler(this, e); 
            }
        }

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
        /// <param name="key">The key for the parameter</param>
        /// <returns>Returns true if the parameter exists</returns>
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
            Output("Planning ...", blanks: 1);
            CanApply = true;
            
            Plan(Root.Nodes);
            
            if( ! CanApply )
            {
                if( Args.IsForced )
                {
                    Output("Warnings have been overriden", 
                            ConsoleColor.Green, 1);

                    CanApply = true;
                }
                else
                {
                    Output("Commmand could not be completed.  Please review the warnings.", 
                            ConsoleColor.Red, 1);
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
                        CanApply = false;

                    Plan(node.Nodes, indent + 1);
                }
            }
        }

        /// <summary>
        /// Applies the command, only called if CanApply = true
        /// </summary>
        protected virtual void Apply()
        {
            Output("Applying ...", blanks: 1);   
            
            Apply(Root.Nodes);
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
                    Apply(node.Nodes, indent + 1);
                }
            }
        }
    }
}