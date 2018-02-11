using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Diagnostics;

namespace Delineate.Fast.Core.Nodes
{
    /// <summary>
    /// Represents the root node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    [DataContract]
    public sealed class RootNode : Node, IDebuggable
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
        public static RootNode Create(CommandContext context)
        {
            return new RootNode()
            {
                Name = ROOT,
                Context = context,
                WorkingDirectory = new DirectoryInfo(Environment.CurrentDirectory)
            };
        }
    }
}