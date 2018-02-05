using System;

namespace Delineate.Fast.Nodes
{
    /// <summary>
    /// Class that presents the a node where action is required
    /// </summary>
    public abstract class ActionNode: Node
    {
        /// <summary>
        /// The Operation to be carried out during the innovation of 
        /// the Plan and Apply Command methods 
        /// </summary>
        /// <returns>Returns the operation for the node</returns>
        public NodeOperation Operation { get; set; }

        /// <summary>
        /// To be implemented for the plan
        /// </summary>
        /// <returns>Returns true if plan has not warnings</returns>
        public abstract bool Plan();

        /// <summary>
        /// To be implemented for the apply
        /// </summary>
        public abstract void Apply();
    }
}