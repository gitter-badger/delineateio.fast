using System.Collections.Generic;

namespace Delineate.Cloud.Fast
{ 
    /// <summary>
    /// The Operation to be taken for a node 
    /// </summary>
    public enum CommandNodeOperation
    {
        None, /// No action to be taken

        Create, /// Create action to be taken

        Delete /// Delete action to be taken
    }
}