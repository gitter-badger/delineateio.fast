using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;

namespace Delineate.Fast.Core.Nodes
{
    /// <summary>
    /// Represents the root node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    public sealed class RootNode : Node
    {
        #region Constants

        /// <summary>
        /// Constant used for the root of the tree
        /// </summary>
        public const string ROOT = "root";

        #endregion

        /// <summary>
        /// Utility function to return a new root node 
        /// when a command is being prepared for execution 
        /// </summary>
        /// <returns></returns>    
        public static RootNode Create(Command command)
        {
            return new RootNode()
            {
                Name = ROOT,
                Command = command,
                WorkingDirectory = new DirectoryInfo(Environment.CurrentDirectory)
            };
        }
    }
}