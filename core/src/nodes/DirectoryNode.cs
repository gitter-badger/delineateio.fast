using System;
using System.IO;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Nodes
{
    /// <summary>
    /// Represents the root node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    public sealed class DirectoryNode : ActionNode
    { 
        /// <summary>
        /// Performs the plan for the directory 
        /// </summary>
        public override bool Plan()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    return PlanCreateDirectory();

                case NodeOperation.Delete:
                    return PlanDeleteDirectory();
                    
                default:    
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Performs the apply for the directory 
        /// </summary>
        public override void Apply()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    ApplyCreateDirectory();
                    break;

                case NodeOperation.Delete:
                    ApplyDeleteDirectory();
                    break;
                    
                default:    
                    throw new ArgumentException();
            }
        }

        #region Plan Methods

        /// <summary>
        /// Plans the creation of a file
        /// </summary>
        /// <returns>Returns true if the file exists</returns>
        private bool PlanCreateDirectory()
        {
            if(WorkingDirectory.Exists)
            {
                Command.Output(string.Format("Directory '{0}' exists", Name), 
                                ConsoleColor.White, 
                                indent: Indent);
            }
            else
            {
                Command.Output(string.Format("Directory '{0}' will be created", Name),
                                ConsoleColor.White, 
                                indent: Indent);
            }

            return false;
        }

        /// <summary>
        /// Plans the deletion of a file
        /// </summary>
        /// <returns>Returns true if the file exists</returns>
        private bool PlanDeleteDirectory()
        {
            if(WorkingDirectory.Exists)
            {
                Command.Output(string.Format("Directory '{0}' will be deleted", Name), 
                                ConsoleColor.Yellow, 
                                indent: Indent);
            }
            else
            {
                Command.Output(string.Format("Directory '{0}' doesn't exist", Name),
                                ConsoleColor.White,  
                                indent: Indent);
            }

            return WorkingDirectory.Exists;
        }

        #endregion

        #region Apply Methods

        /// <summary>
        /// Applies the create directory
        /// </summary>
        private void ApplyCreateDirectory()
        {
            if(Directory.Exists(WorkingDirectory.FullName))
            {
                Command.Output(string.Format("Directory '{0}' existed", Name), 
                                ConsoleColor.White,
                                indent: Indent);
            }
            else
            {
                Directory.CreateDirectory(WorkingDirectory.FullName);
                Command.Output(string.Format("Directory '{0}' created", Name), 
                                ConsoleColor.Green, 
                                indent: Indent);
            }
        }

        /// <summary>
        /// Applies the delete directory
        /// </summary>
        private void ApplyDeleteDirectory()
        {
            if(Directory.Exists(WorkingDirectory.FullName))
            {
                Directory.Delete(WorkingDirectory.FullName, true);
                Command.Output(string.Format("Directory '{0}' deleted", Name), 
                                ConsoleColor.Green, 
                                indent: Indent);
            }
            else
            {
                Command.Output(string.Format("Directory '{0}' didn't exist", Name),
                                ConsoleColor.White,
                                indent: Indent);
            }
        }

        #endregion
    }
}