using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Logging;

namespace Delineate.Fast.Core.Nodes
{
    /// <summary>
    /// Represents a node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    [DataContract]
    [KnownType(typeof(FileNode))]
    [KnownType(typeof(DirectoryNode))]
    public abstract class Node : ILoggable
    {
        #region Properties

        /// <summary>
        /// Reference to the parent node
        /// </summary>
        /// <returns>
        /// An object reference to the parent node enabling
        /// traversal, for the root parent is null
        /// </returns>
        public Node Parent { get; private set; }

        /// <summary>
        /// The context of the execution
        /// </summary>
        /// <returns>Returns the current context</returns>
        public CommandContext Context { get; set; }

        /// <summary>
        /// Name to be used for node
        /// </summary>
        /// <returns>Returns the name of the node</returns>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Collection of any child nodes
        /// </summary>
        /// <returns>
        /// Returns an unsorted list of any child nodes, if there
        /// are n o nodes then null if returned
        /// </returns>
        [DataMember]
        public IList<Node> Nodes { get; set; }

        /// <summary>
        /// Return the current directory
        /// </summary>
        protected DirectoryInfo WorkingDirectory { get; set; }
        
        #endregion 

        /// <summary>
        /// Adds a child node to the current node
        /// </summary>
        /// <param name="name">Name of the node to be added</param>
        /// <param name="operation">The operation for the node added</param>
        /// <returns>The newly added node</returns>
        public T Add<T>(string name, NodeOperation operation = NodeOperation.Create) where T: ActionNode, new()
        {
            if(Nodes == null)
                Nodes = new List<Node>();

            T node = new T()
            {
                Name = name,
                Operation = operation,
                WorkingDirectory = GetWorkingDirectory<T>(name),
                Parent = this,
                Context = Context
            };

            Nodes.Add(node);

            return node;
        }
        
        /// <summary>
        /// Gets the working directory to be set on 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private DirectoryInfo GetWorkingDirectory<T>(string name) where T: Node, new()
        {
            if( typeof(T) == typeof(DirectoryNode) )
            {
                string path = Path.Combine(WorkingDirectory.FullName, name);
                return new DirectoryInfo(path);
            }
            else
            {
                return WorkingDirectory;
            }
        }
    }
}