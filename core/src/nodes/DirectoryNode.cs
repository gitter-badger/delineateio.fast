using System;
using System.IO;
using System.Collections.Generic;
using Delineate.Fast.Core.Messages;

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
                Outputs.Normal(string.Format("Directory '{0}' exists", Name));
            }
            else
            {
                Outputs.Normal(string.Format("Directory '{0}' will be created", Name));
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
                Outputs.Warning(string.Format("Directory '{0}' will be deleted", Name));
            }
            else
            {
                Outputs.Normal(string.Format("Directory '{0}' doesn't exist", Name));
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
                Outputs.Normal(string.Format("Directory '{0}' existed", Name));
            }
            else
            {
                Directory.CreateDirectory(WorkingDirectory.FullName);
                Outputs.Success(string.Format("Directory '{0}' created", Name));
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
                Outputs.Success(string.Format("Directory '{0}' deleted", Name));
            }
            else
            {
                Outputs.Normal(string.Format("Directory '{0}' didn't exist", Name));
            }
        }

        #endregion
    }
}