using System;
using System.Collections.Generic;

namespace Delineate.Fast.Core.Nodes
{ 
    /// <summary>
    /// The Operation to be taken for a node 
    /// </summary>
    public enum NodeOperation
    {
        None, /// No action to be taken

        Create, /// Create action to be taken

        Delete /// Delete action to be taken
    }
}