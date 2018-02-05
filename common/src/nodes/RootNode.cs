using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Fast.Nodes
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
        public static RootNode Create()
        {
            return new RootNode()
            {
                Name = ROOT,
                Operation = NodeOperation.None,
                WorkingDirectory = new DirectoryInfo(Environment.CurrentDirectory)
            };
        }
        
        /// <summary>
        /// Null implementation of Plan 
        /// </summary>
        /// <param name="warnings">The collection of warnings</param>
        public override void Plan(List<string> warnings){}

        /// <summary>
        /// Null implementation of Apply
        /// </summary>
        /// <param name="warnings">The collection of warnings</param>
        public override void Apply(){} 

    }
}