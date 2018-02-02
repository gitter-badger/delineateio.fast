using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{
    /// <summary>
    /// Represents a node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    public sealed class CommandNode
    {
        #region Constants

        /// <summary>
        /// Constant used for the root of the tree
        /// </summary>
        public const string ROOT = "root";

        #endregion

        #region Properties

        /// <summary>
        /// Reference to the parent node
        /// </summary>
        /// <returns>
        /// An object reference to the parent node enabling
        /// traversal, for the root parent is null
        /// </returns>
        public CommandNode Parent { get; private set; }

        /// <summary>
        /// Name to be used for node
        /// </summary>
        /// <returns>Returns the name of the node</returns>
        public string Name { get; set; }

        /// <summary>
        /// The type of the node that determines the processing
        /// during the Plan and Apply invocation  
        /// </summary>
        /// <returns>Returns the type (e.g. Directory, File)</returns>
        public CommandNodeType Type { get; set; }

        /// <summary>
        /// The Operation to be carried out during the innovation of 
        /// the Plan and Apply Command methods 
        /// </summary>
        /// <returns></returns>
        public CommandNodeOperation Operation { get; set; }

        /// <summary>
        /// Collection of any child nodes
        /// </summary>
        /// <returns>
        /// Returns an unsorted list of any child nodes, if there
        /// are n o nodes then null if returned
        /// </returns>
        public List<CommandNode> Nodes { get; private set; }

        #endregion 
        
        /// <summary>
        /// Utility function to return a new root node 
        /// when a command is being prepared for execution 
        /// </summary>
        /// <returns></returns>    
        public static CommandNode CreateRoot()
        {
            return new CommandNode()
            {
                Name = ROOT,
                Type = CommandNodeType.Root,
                Operation = CommandNodeOperation.None
            };
        }

        /// <summary>
        /// Adds a child node to the current node
        /// </summary>
        /// <param name="node">The node to be added as a child</param>
        /// <returns>Returns the added node</returns>
        /// <remarks>Instantiates the collection if required</remarks>
        private CommandNode Add(CommandNode node)
        {
            if(Nodes == null)
                Nodes = new List<CommandNode>();

            node.Parent = this;

            Nodes.Add(node);

            return node;
        }

        /// <summary>
        /// Adds a child node to the current node
        /// </summary>
        /// <param name="name">Name of the node to be added</param>
        /// <param name="type">Type of the node to be added</param>
        /// <param name="operation">The operation for the node added</param>
        /// <returns>The newly added node</returns>
        public CommandNode Add(string name, CommandNodeType type = CommandNodeType.Directory, 
                                    CommandNodeOperation operation = CommandNodeOperation.Create)
        {
            CommandNode node = new CommandNode()
            {
                Name = name,
                Type = type,
                Operation = operation 
            };

            return Add(node);
        }
    }
}